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
    //serialize guid and datetime to improve readability
    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
    BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

    //retrive IConfiguration service instance to read configuration values from appsettings, .env etc
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    //retrieve configuration instance of our MongoDbConfig class
    var mongoSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
    //create new MongoClient instance using connection string from mongoSettings. 
    //All requests of IMongoClient will use the same instance of this MongoClient instead of creating new ones
    return new MongoClient(mongoSettings.ConnectionString);
});



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

app.Run();

DotEnv.Load();
