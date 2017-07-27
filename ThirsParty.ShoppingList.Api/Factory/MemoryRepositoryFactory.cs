using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPM = ThirdParty.ShoppingList.Service.Model;
using TPI = ThirdParty.ShoppingList.Service.Interfaces;
using TP=ThirdParty.ShoppingList.Service;

namespace ThirdParty.ShoppingList.Api.Factory
{
   
        public class MemoryRepositoryModule : NinjectModule {
            public override void Load()
            {
                Bind<TPI.IItem>().To<TPM.ShoppingItem>();
                Bind<TPI.IStorage>().To<TP.MemoryCacheStorage>();
                Bind<TPI.IRepository<TPI.IItem>>().To<TP.Repository>();
            }
        }
    
}