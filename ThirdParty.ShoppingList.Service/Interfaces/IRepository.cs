using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service.Interfaces
{
    public interface IRepository<T> where T: class, new()
    {
        bool Insert(T item);
        bool Update(T item);
        bool Delete(T item);
        T Get(T item);
        IEnumerable<T> Get();
    
    }
}
