using Microsoft.Extensions.Caching.Memory;
using MyPermutationSolution.Server.Interfaces;

namespace MyPermutationSolution.Server.Services
{
    public class MemoryCacheService: IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string cacheKey)
        {
            return _cache.TryGetValue(cacheKey, out T value) ? value : default;
        }

        public void Set<T>(string cacheKey, T value, TimeSpan expiration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            _cache.Set(cacheKey, value, cacheEntryOptions);
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public bool Exists(string cacheKey)
        {
            return _cache.TryGetValue(cacheKey, out _);
        }
    }
}
