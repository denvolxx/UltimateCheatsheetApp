using MongoDBModels;

namespace MongoDBService.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddProductAsync(Product product);
    }
}
