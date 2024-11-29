using Microsoft.AspNetCore.Mvc;
using UltimateCheatsheetApp.Services;
using UltimateCheatsheetApp.Services.RedisCache;

namespace UltimateCheatsheetApp.Controllers
{
    public class DocUploadController(DocUploadProcuder _producer, IRedisCacheService _cacheService) : Controller
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileBytes = stream.ToArray();
                var fileKey = Guid.NewGuid().ToString();

                await _cacheService.CacheFileAsync(fileKey, fileBytes);
                await _producer.FileUploadProducer("file-uploader", fileKey);
            }

            return Ok("Data export from file started");
        }

    }
}
