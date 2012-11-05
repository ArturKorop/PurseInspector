using System;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Repository;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                       ? null
                       : (IController)_ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            _ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}