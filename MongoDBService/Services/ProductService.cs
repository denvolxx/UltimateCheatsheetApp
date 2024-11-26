using MongoDB.Driver;
using MongoDBModels;
using MongoDBService.Data;

namespace MongoDBService.Services
{
    public class ProductService(MongoDataContext context) : IProductService
    {
        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> response = await context.Products.Find(_ => true).ToListAsync();

            if (response == null)
            {
                throw new Exception("Unable to retrieve products");
            }
            return response;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            Product? response = await context.Products.Find(i => i.Id == id).FirstOrDefaultAsync();

            if (response == null)
            {
                throw new Exception("Unable to retrieve product");
            }

            return response;
        }
        public async Task AddProductAsync(Product product)
        {
            await context.Products.InsertOneAsync(product);
        }
    }
}
