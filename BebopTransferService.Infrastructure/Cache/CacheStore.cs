namespace BebopTransferService.Infrastructure.Cache;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class CacheStore(IDistributedCache distributedCache, ILogger<CacheStore> logger) : ICacheStore
{
    public async Task<T?> GetAsync<T>(string key, JsonSerializerOptions? jsonSerializerOptions = null) where T : class
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }
        var value = await distributedCache.GetAsync(key);
        if (value is null || value.Length == 0)
        {
            logger.LogInformation("Key {key} not found", key);
            return default;
        }
        return JsonSerializer.Deserialize<T>(value, jsonSerializerOptions);
    }

    public async Task SaveAsync<T>(string key, T value, DistributedCacheEntryOptions options, JsonSerializerOptions? jsonSerializerOptions = null) where T : class
    {
        if(string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }
        if(value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var result = JsonSerializer.SerializeToUtf8Bytes(value, jsonSerializerOptions);
        await distributedCache.SetAsync(key, result);
    }
}
