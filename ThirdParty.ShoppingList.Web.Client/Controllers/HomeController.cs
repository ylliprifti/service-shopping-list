using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ThirdParty.ShoppingList.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        private static string _token = string.Empty;
        // GET: Home
        public ActionResult Index()
        {
            return View();

        }
        

    }
}