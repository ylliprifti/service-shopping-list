using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.ShoppingList.Models
{
    public class TokenModel
    {
        public string access_token;
        public string token_type;
        public long expires_in;
    }
}
