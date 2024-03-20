namespace BebopTransferService.Infrastructure.Cache;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

internal interface ICacheStore
{
    Task<T?> GetAsync<T>(string key, JsonSerializerOptions? jsonSerializerOptions = null) where T : class;

    Task SaveAsync<T>(string key, T value, DistributedCacheEntryOptions options, JsonSerializerOptions? jsonSerializerOptions = null) where T : class;
}
