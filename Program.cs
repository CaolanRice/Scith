using Scith.Repositories;
using dotenv.net;
using MongoDB.Driver;
using Scith.Configurations;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//prevents .NET from removing async suffix from methods during runtime
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//only create one instance of repository over the lifetime of the service/application
builder.Services.AddSingleton<InterfaceItemsRepository, MongoDbRepository>();
//setup IMongoClient for dependency injection
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    // Serialize guid and datetime to improve readability
    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
    BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

    // Retrieve IConfiguration service instance to read configuration values from appsettings, .env, etc.
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    // Retrieve the MongoDbConfig section directly without using connection string name
    var mongoSettings = new MongoDbConfig();
    configuration.GetSection("MongoDbConfig").Bind(mongoSettings);

    // Create new MongoClient instance using the connection string from mongoSettings.
    // All requests of IMongoClient will use the same instance of this MongoClient instead of creating new ones.
    return new MongoClient(mongoSettings.ConnectionString);
});

// Health check for REST APIs
var mongoDbConfig = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.AddHealthChecks()
    .AddMongoDb(mongoDbConfig.ConnectionString);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthcheck");

app.Run();

DotEnv.Load();
