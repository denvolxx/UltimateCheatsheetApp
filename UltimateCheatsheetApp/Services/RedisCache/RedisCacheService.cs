using StackExchange.Redis;

namespace UltimateCheatsheetApp.Services.RedisCache
{
    public class RedisCacheService(IConnectionMultiplexer _redis) : IRedisCacheService
    {
        public async Task CacheFileAsync(string fileKey, byte[] fileData)
        {
            var db = _redis.GetDatabase(); 
            await db.StringSetAsync(fileKey, fileData);
        }

        public async Task<byte[]> GetCachedFileAsync(string fileKey)
        {
            var db = _redis.GetDatabase(); 
            var result = await db.StringGetAsync(fileKey);

            return result;
        }
    }
}
