using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;

using System.Configuration;

namespace TravelCard.Tests.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel = new StandardKernel(new NinjectBindingDefinitions());
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)_kernel.Get(controllerType);
        }

        private class NinjectBindingDefinitions : NinjectModule
        {
            public override void Load()
            {
                Bind(typeof(Ideal.DomainModel.Abstract.IPartsRepository<>))
                    .To(typeof(Ideal.DomainModel.Repositories.PartRepository));
                 
            }
        }
    }
}