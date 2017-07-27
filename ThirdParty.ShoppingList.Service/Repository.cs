using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.ShoppingList.Service.Model;

namespace ThirdParty.ShoppingList.Service
{
    public class Repository : Interfaces.IRepository<Interfaces.IItem>
    {
        private readonly Interfaces.IStorage _storage;

        public Repository(Interfaces.IStorage storage) {
            _storage = storage;
        }
        
        /// <summary>
        /// Deletes exising item identified by name
        /// </summary>
        /// <param name="item">Will look for item.Name and delete item from storage. Item.Name should not be empty </param>
        /// <returns>Returns true if delete is successfull. Returns false if item does not exist. Will throw an exeption if delete fails.</returns>
        public bool Delete(Interfaces.IItem item)
        {
            if (item.IsEmpty()) return false;

            if (!_storage.Contains(item.Name)) return false;
            _storage.Remove(item.Name); return true;
        }

        /// <summary>
        /// This will retreive all items from the underlying storage
        /// </summary>
        /// <returns>
        /// Returns all items from the storage. If items or not of type IItem will return null
        /// </returns>
        public IEnumerable<Interfaces.IItem> Get() => _storage.AllItems.Cast<Interfaces.IItem>();
        
        /// <summary>
        /// Get item uset to retreive a single item identified by item.Name
        /// </summary>
        /// <param name="item">The item to retreive, item.Name should not be emtpy </param>
        /// <returns>
        /// returns an item with the same name as the input parameter and with the correct Quantity of an exisint Item.
        /// returns null if item does not exist
        /// </returns>
        public Interfaces.IItem Get(Interfaces.IItem item) => _storage.Get(item.Name) as Interfaces.IItem;
       
        /// <summary>
        /// Inerts a new item if it does not exists, or updated the quantity if it exists.
        /// </summary>
        /// <param name="item">Item to insert.</param>
        /// <returns> Will return true if item is inserted or quantity is updated. False if no actions</returns>
        public bool Insert(Interfaces.IItem item)
        {
            if (item.IsEmpty()) return false;

            if (_storage.Contains(item.Name))
            {
                var current = _storage[item.Name] as Interfaces.IItem;
                current.Quantity += item.Quantity;
                _storage[item.Name] = current;
                return true;
            }

            _storage[item.Name] = item;
            return true;
        }

        /// <summary>
        /// Updates an existing item to new value
        /// </summary>
        /// <param name="item">The new value for item</param>
        /// <returns></returns>
        public bool Update(Interfaces.IItem item)
        {
            if (item.IsEmpty()) return false;
            
            if (!_storage.Contains(item.Name)) return false;
            
            _storage[item.Name] = item; return true;
        }
    }

    public static class ItemExtention {
        public static bool IsEmpty(this Interfaces.IItem context) {
            return (context == null || string.IsNullOrEmpty(context.Name));
        }
    }

}
