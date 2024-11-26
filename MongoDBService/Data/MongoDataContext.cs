using MongoDB.Driver;
using MongoDBModels;

namespace MongoDBService.Data
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database;
        public MongoDataContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    }
}
