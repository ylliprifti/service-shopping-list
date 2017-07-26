using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using ThirdParty.ShoppingList.Service;
using ThirdParty.ShoppingList.Service.Model;

namespace ThirdParty.ShoppingList.Api.Controllers
{
    public class ShoppingListController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult GetAnonymous()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }


        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }

        private MemoryRepository _repository = new MemoryRepository();



        [Authorize]
        [Route("api/shoppinglist/{name}")]
        public IHttpActionResult Get(string name)
        {
              return Ok( _repository.Get(new ShoppingItem { Name = name }));
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get());
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult Post([FromBody]ShoppingItem value)
        {
          return Ok(_repository.Insert(value));
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult Put([FromBody]ShoppingItem value)
        {
             return Ok(_repository.Update(value));
        }

        [Authorize(Roles = "admin")]
        [Route("api/shoppinglist/{name}")]
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
           return Ok(_repository.Delete(new ShoppingItem { Name = name }));
        }
    }
}