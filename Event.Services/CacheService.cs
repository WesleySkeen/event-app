using Microsoft.Extensions.Caching.Memory;

namespace Event.Services
{
    public class  CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key)
        {
            T obj;
            if (!_cache.TryGetValue(key, out obj))
            {
                return default(T);
            }

            return obj;
        }

        public void Set(string key, object value)
        {
            _cache.Set(key, value);
        }
    }

    public interface ICacheService
    {
        T Get<T>(string key);
        
        void Set(string key, object value);
    }
}