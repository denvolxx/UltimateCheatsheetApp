namespace UltimateCheatsheetApp.Services.RedisCache
{
    public interface IRedisCacheService
    {
        Task CacheFileAsync(string key, byte[] fileData); 
        Task<byte[]> GetCachedFileAsync(string key);
    }
}
