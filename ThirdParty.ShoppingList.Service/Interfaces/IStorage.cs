using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service.Interfaces
{
    public interface IStorage
    {
        void Remove(string key);
        bool Contains(string key);
        object Get(string key);
        IEnumerable<string> AllKeys { get; }
        IEnumerable<object> AllItems { get; }
        object this[string key] { get; set; }

    }
}
