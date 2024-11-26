using ApplicationDTO.Mongo.Products;
using Microsoft.AspNetCore.Mvc;
using MongoDBModels;
using MongoDBService.Services;
using UltimateCheatsheetApp.Controllers.Base;

namespace UltimateCheatsheetApp.Controllers
{
    public class ProductController(IProductService context) : BaseApiController
    {
        [HttpGet("all")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await context.GetAllAsync();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int productId)
        {
            var product = await context.GetByIdAsync(productId);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await context.AddProductAsync(product);
            return Ok(product);
        }
    }
}
