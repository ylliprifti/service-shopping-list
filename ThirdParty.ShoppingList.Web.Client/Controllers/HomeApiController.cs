using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ThirdParty.ShoppingList.Web.Client.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeApiController : ApiController
    {
        private static string _token = string.Empty;
        Checkout.APIClient _client = new Checkout.APIClient();

        [HttpPost]
        [Route("login/{username}/{password}")]
        public IHttpActionResult Login(string username, string password)
        {
            var tokenResult = _client.ShoppingListService.Login(username, password);

            tokenResult.Wait();

            _token = tokenResult.Result.Model.access_token;

            return Ok(_token);
        }

        [HttpPost]
        [Route("additem")]
        public  IHttpActionResult AddItem([FromBody]Checkout.ApiServices.ShoppingList.Models.ShoppingItem product)
        {

            if (string.IsNullOrEmpty(_token)) return Ok("Please login!");

            if (product == null || string.IsNullOrEmpty(product.Name) || product.Quantity <= 0)
                return Ok("Check Shopping Item data!");

            var addResult = _client.ShoppingListService.AddItem(_token, product);

            addResult.Wait();

            if (addResult.Result.Model) return Ok("Item Added!");
            return Ok("Item add failed!");
        }

        [HttpPost]
        [Route("updateitem")]
        public  IHttpActionResult UpdateItem([FromBody]Checkout.ApiServices.ShoppingList.Models.ShoppingItem product)
        {

            if (string.IsNullOrEmpty(_token)) return Ok("Please login!");

            if (product == null || string.IsNullOrEmpty(product.Name) || product.Quantity <= 0)
                return Ok("Check Shopping Item data!");

            var addResult = _client.ShoppingListService.UpdateItem(_token, product);

            addResult.Wait();

            if (addResult.Result.Model) return Ok("Item Updated!");
            return Ok("Item update failed!");
        }

        [HttpPost]
        [Route("deleteitem/{name}")]
        public IHttpActionResult DeleteItem(string name)
        {

            if (string.IsNullOrEmpty(_token))
                return Ok("Please login!");

            if (string.IsNullOrEmpty(name))
                return Ok("Check Shopping Item Name!");

            var addResult =  _client.ShoppingListService.DeleteItem(_token, name);

            addResult.Wait();

            if (addResult.Result.Model) return Ok("Item deleted!");
            return Ok("Item delete failed!");
        }

        [HttpGet]
        [Route("item/{name}")]
        public IHttpActionResult GetItem(string name)
        {

            if (string.IsNullOrEmpty(_token))
                return Ok("Please login!");

            if (string.IsNullOrEmpty(name))
                return Ok("Check Shopping Item Name!");

            var addResult = _client.ShoppingListService.GetItem(_token, name);

            addResult.Wait();

            if (addResult.Result.Model == null) return Ok("Item does not exist!");
            return Ok(addResult.Result.Model);
        }


        [HttpGet]
        [Route("items")]
        public IHttpActionResult GetItems()
        {

            if (string.IsNullOrEmpty(_token))
                return Ok("Please login!");
            
            var addResult = _client.ShoppingListService.GetItems(_token);

            addResult.Wait();

            if (addResult.Result.Model == null) return Ok("Something went wrong");
            return Ok(addResult.Result.Model);
        }


    }

}
