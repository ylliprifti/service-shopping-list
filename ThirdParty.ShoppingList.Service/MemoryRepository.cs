using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.ShoppingList.Service.Model;

namespace ThirdParty.ShoppingList.Service
{
    public class MemoryRepository : Interfaces.IRepository<Model.ShoppingItem>
    {
        private MemoryCache _cache;

        public MemoryRepository() {
            _cache = MemoryCache.Default;
        }
        
        public bool Delete(ShoppingItem item)
        {
            if (!_cache.Contains(item.Name)) return false;
            _cache.Remove(item.Name); return true;
        }

        public IEnumerable<ShoppingItem> Get()
        {
            var items = new List<ShoppingItem>();
           var allKeys = _cache.Select(x => x.Key).ToList();
            allKeys.ForEach(key => items.Add(_cache[key] as ShoppingItem));
            return items;
        }

        public ShoppingItem Get(ShoppingItem item)
        {
            return _cache.Get(item.Name) as ShoppingItem;
        }

        public bool Insert(ShoppingItem item)
        {
            if (string.IsNullOrEmpty(item.Name)) return false;
           if (_cache[item.Name] != null)
            {
                var current = _cache[item.Name] as ShoppingItem;
                current.Quantity += item.Quantity;
                _cache[item.Name] = current;
                return true;
            }

            _cache[item.Name] = item;
            return true;
        }

        public bool Update(ShoppingItem item)
        {
            bool exists = _cache[item.Name] != null;
            if (!exists) return false;
            _cache[item.Name] = item;
            return true;

        }
    }
}
