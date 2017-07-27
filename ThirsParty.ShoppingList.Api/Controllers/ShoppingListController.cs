using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TP=ThirdParty.ShoppingList.Service;
using TPM=ThirdParty.ShoppingList.Service.Model;
using TPI = ThirdParty.ShoppingList.Service.Interfaces;


namespace ThirdParty.ShoppingList.Api.Controllers
{
    public class ShoppingListController : ApiController
    {

        private readonly TPI.IRepository<TPI.IItem> _repository;

        public ShoppingListController(TPI.IRepository<TPI.IItem> repository) {
            _repository = repository;
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/status")]
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
        
        [Authorize]
        [Route("api/shoppinglist/{name}")]
        public IHttpActionResult Get(string name)
        {
              return Ok( _repository.Get(new TPM.ShoppingItem { Name = name }));
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get());
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult Post([FromBody]TPM.ShoppingItem value)
        {
          return Ok(_repository.Insert(value));
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult Put([FromBody]TPM.ShoppingItem value)
        {
             return Ok(_repository.Update(value));
        }

        [Authorize(Roles = "admin")]
        [Route("api/shoppinglist/{name}")]
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
           return Ok(_repository.Delete(new TPM.ShoppingItem { Name = name }));
        }
    }
}