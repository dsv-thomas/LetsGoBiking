using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    internal class Cache<T> : ICache<T> where T : new()
    {
        ObjectCache cache = MemoryCache.Default;
        public T Get(string CacheItem)
        {
            return Get(CacheItem, ObjectCache.InfiniteAbsoluteExpiration);
        }


        public T Get(string CacheItem, DateTimeOffset dt)
        {
            if (cache.Get(CacheItem) == null)
            {
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = dt
                };
                 cache.Add(CacheItem, new T(), cacheItemPolicy);       
            }
            return (T)cache.Get(CacheItem);
        }

        public T Get(string CacheItem, double dt_seconds)
        {
            return Get(CacheItem, DateTimeOffset.Now.AddSeconds(dt_seconds));
        }

        public T Get(string CacheItem, double dt_seconds, Dictionary<String, String> param)
        {
            if (cache.Get(CacheItem) == null)
            {
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(dt_seconds)
                };
                //https://stackoverflow.com/questions/19059272/c-sharp-how-to-initialize-generic-class-with-object-of-type-type
                //https://social.msdn.microsoft.com/Forums/en-US/626edcf3-3ea0-4c59-96c7-738728cfd6d9/initialize-generic-type-with-parameter-constructor?forum=aspcsharp
                cache.Add(CacheItem, (T)Activator.CreateInstance(typeof(T), param), cacheItemPolicy);

            }
            return (T)cache.Get(CacheItem);
        }
    }
}
