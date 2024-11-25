using MongoDB.Driver;
using MongoDBService.Data;
using MongoDBService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
