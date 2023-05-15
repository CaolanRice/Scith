namespace Scith.Configurations
{
    public class MongoDbConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }

    }
}
