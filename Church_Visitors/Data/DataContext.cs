using MongoDB.Driver;

namespace Church_Visitors.Data
{
    public class DataContext
    {
        public IMongoDatabase Database { get; }

        public DataContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }
    }
}
