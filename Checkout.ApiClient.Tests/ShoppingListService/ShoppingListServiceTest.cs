using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL = Checkout.ApiServices.ShoppingList;

namespace Tests.ShoppingListService
{
    [TestFixture]
    public class ShoppingListServiceTest
    {
        private SL.ShoppingListService _service = null;
        private string _token = null;
        [SetUp]
        public void Setup() {
            _service = new SL.ShoppingListService();
            var tokenBearer = _service.Login("admin", "admin");
            tokenBearer.Wait();
            _token = tokenBearer.Result.Model.access_token;
        }
        [Test]
        public void Login() {
            var token = _service.Login("admin", "admin");
            token.Wait(); var result = token.Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsNotNull(result.Model.access_token);
        }

        [Test]
        public void GetSingleItem()
        {
            AddItem();
            var item = _service.GetItem(_token, "Milk");
            item.Wait();
            Assert.IsNotNull(item.Result.Model);
            Assert.IsTrue(item.Result.Model.Quantity > 0);
        }

        [Test]
        public void GetListItem()
        {
            AddItem();
            var item = _service.GetItems(_token);
            item.Wait();
            Assert.AreNotEqual(item.Result.Model.Count(), 0);
        }

        [Test]
        public void AddItem()
        {
            var item = _service.AddItem(_token, new SL.Models.ShoppingItem { Name = "Milk", Quantity =1  } );
            item.Wait();
            Assert.IsTrue(item.Result.Model);
        }

        [Test]
        public void DeleteItem()
        {
            AddItem();
            var item = _service.DeleteItem(_token, "Milk");
            item.Wait();
            Assert.IsTrue(item.Result.Model);
        }

        [Test]
        public void UpdateItem()
        {
            AddItem();
            var item = _service.UpdateItem(_token,new SL.Models.ShoppingItem { Name = "Milk", Quantity = 999 } );
            item.Wait();
            Assert.IsTrue(item.Result.Model);
            var newItem = _service.GetItem(_token, "Milk");
            newItem.Wait();
            Assert.IsTrue(newItem.Result.Model.Quantity == 999);
        }

    }
}
