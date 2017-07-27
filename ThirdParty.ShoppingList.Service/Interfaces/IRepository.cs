using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service.Interfaces
{
    public interface IRepository<IItem>
    {
        bool Insert(IItem item);
        bool Update(IItem item);
        bool Delete(IItem item);
        IItem Get(IItem item);
        IEnumerable<IItem> Get();
    
    }
}
