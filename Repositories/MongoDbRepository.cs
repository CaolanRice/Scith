using MongoDB.Bson;
using MongoDB.Driver;
using Scith.Entities;



namespace Scith.Repositories
{
    public class MongoDbRepository : InterfaceItemsRepository
    {
        private static readonly string databaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "scithdb";
        private static readonly string collectionName = Environment.GetEnvironmentVariable("MONGO_DB_COLLECTION") ?? "items";

        private readonly IMongoCollection<Item> itemCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;



        //MongoDB.Driver dependency
        public MongoDbRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemCollection = database.GetCollection<Item>(collectionName);

        }

        public async Task CreateItemAsync(Item item)
        {
            //implement async version of the methods that IMongoCollection offers
            await itemCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            //search through item collection to filter out item that matches id of the item passed as parameter
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            //return filtered item
            return await itemCollection.Find(filter).SingleOrDefaultAsync();
        }

        //retrieve all documents from collection and return as async sequence of item objects
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            //find all documents and return as list
            return await itemCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(storedItem => storedItem.Id, item.Id);
            //replace 
            await itemCollection.ReplaceOneAsync(filter, item);
        }
    }
}