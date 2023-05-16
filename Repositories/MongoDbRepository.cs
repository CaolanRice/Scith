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

        public void CreateItem(Item item)
        {
            itemCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            //search through item collection to filter out item that matches id of the item passed as parameter
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            //return filtered item
            return itemCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            //find all documents and return as list
            return itemCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(storedItem => storedItem.Id, item.Id);
            //replace 
            itemCollection.ReplaceOne(filter, item);
        }
    }
}