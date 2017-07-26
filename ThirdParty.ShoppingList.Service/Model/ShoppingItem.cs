using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.ShoppingList.Service.Model
{
    public class ShoppingItem
    {
        public ShoppingItem() { }
        public ShoppingItem(string name, int quantity) {
            Name = name;
            Quantity = quantity;
        }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            var refObj = obj as ShoppingItem;
            if(refObj == null) return base.Equals(obj);
            return Name.AreNullOrEqual(refObj.Name) && Quantity == refObj.Quantity;
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(Name)) return base.GetHashCode();
            return Name.GetHashCode() + Quantity.GetHashCode();
        }

    }
    internal static class NullStrExtention {
        public static bool AreNullOrEqual(this string context, string otherString) {
            if (string.IsNullOrEmpty(context) && string.IsNullOrEmpty(otherString))
                return true;
            if (string.IsNullOrEmpty(context) || string.IsNullOrEmpty(otherString))
                return false;
            return context.Equals(otherString, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
