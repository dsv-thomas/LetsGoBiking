using System;
using System.Collections.Generic;

namespace ProxyCache
{
    public interface ICache<T>
    {
        T Get(string CacheItem);
        T Get(string CacheItem, double dt_seconds);
        T Get(string CacheItem, DateTimeOffset dt);

        T Get(string CacheItem, double dt_seconds, Dictionary<String, String> param);
    }
}
