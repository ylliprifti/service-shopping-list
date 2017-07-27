using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service.Interfaces
{
    public interface IItem
    {
        string Name { get; set; }
        int Quantity { get; set; }
    }
}
