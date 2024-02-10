using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);
        T Set<T>(string key, T value);
        T Set<T>(string key, T value, TimeSpan? slidingExpiration, TimeSpan? absoluteExpiration);

        Task<T> GetAsync<T>(string key);
        Task<T> SetAsync<T>(string key, T value);
        Task<T> SetAsync<T>(string key, T value, TimeSpan? slidingExpiration, TimeSpan? absoluteExpiration);

        void Remove(string key);
        Task RemoveAsync(string key);
    }
}
