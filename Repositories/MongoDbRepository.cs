using MongoDB.Driver;
using Scith.Entities;



namespace Scith.Repositories
{
    public class MongoDbRepository : InterfaceItemsRepository
    {
        private static readonly string databaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "scithdb";
        private static readonly string collectionName = Environment.GetEnvironmentVariable("MONGO_DB_COLLECTION") ?? "items";
        private readonly IMongoCollection<Item> itemCollection;



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
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}