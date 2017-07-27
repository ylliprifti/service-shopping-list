using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service
{
    public class MemoryCacheStorage:Interfaces.IStorage
    {
        private readonly MemoryCache _cache;

        public MemoryCacheStorage() {
            _cache = MemoryCache.Default;
        }
        public MemoryCacheStorage(string cacheName)
        {
            _cache = new MemoryCache(cacheName);
        }
        
        public void Remove(string key) => _cache.Remove(key);
        public bool Contains(string key) => _cache.Contains(key);
        public object Get(string key) => _cache.Get(key);
        public IEnumerable<string> AllKeys => _cache.Select(x => x.Key);
        public IEnumerable<object> AllItems => _cache.Select(x => x.Value);
        public object this[string key] { get { return _cache[key]; } set { _cache[key] = value; } }
    }
}
