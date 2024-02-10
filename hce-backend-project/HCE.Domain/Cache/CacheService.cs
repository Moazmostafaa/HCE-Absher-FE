using HCE.Interfaces.Cache;
using HCE.Utility.CommonModels;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly RedisSetting _redisSetting;

        public CacheService(IDistributedCache cache, RedisSetting redisSetting)
        {
            _cache = cache;
            _redisSetting = redisSetting;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(_redisSetting.DefaultSlidingExpirationInMinutes),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_redisSetting.DefaultAbsoluteExpirationInMinutes),
            };

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return value;
        }

        public T Set<T>(string key, T value, TimeSpan? slidingExpiration, TimeSpan? absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration,
                AbsoluteExpirationRelativeToNow = absoluteExpiration,
            };

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(_redisSetting.DefaultSlidingExpirationInMinutes),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_redisSetting.DefaultAbsoluteExpirationInMinutes),
            };

            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);

            return value;
        }

        public async Task<T> SetAsync<T>(string key, T value, TimeSpan? slidingExpiration, TimeSpan? absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration,
                AbsoluteExpirationRelativeToNow = absoluteExpiration,
            };

            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);

            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
