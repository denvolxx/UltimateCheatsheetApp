using MongoDB.Driver;
using MongoDBService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
