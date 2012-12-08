using System;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Repository;
using Domain.User;
using Ninject;

namespace WebUI.Infrastructure
{
    /// <summary>
    /// Contoller factory which base on Ninject
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;
        /// <summary>
        /// Constructor of controllers factory
        /// </summary>
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
            _ninjectKernel.Bind<IOperationRepository>().To<EFOperationRepository>();
            _ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}