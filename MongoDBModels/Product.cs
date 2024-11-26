using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBModels
{
    public class Product
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
