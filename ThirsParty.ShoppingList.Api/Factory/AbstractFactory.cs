using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThirdParty.ShoppingList.Api.Factory
{

    public static class AbstractFactory
    {
        public static NinjectModule MemoryRepository() {
            return new MemoryRepositoryModule();
        }

        public static NinjectModule CosmoDocDBRepository() {
            return new CosmoDocDBRepository();
        }

    }
}