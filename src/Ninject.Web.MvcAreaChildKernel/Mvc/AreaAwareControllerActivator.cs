using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareControllerActivator : IControllerActivator
    {
        private readonly IKernelResolver kernelResolver;

        public AreaAwareControllerActivator(IKernelResolver kernelResolver)
        {
            if (kernelResolver == null) throw new ArgumentNullException("kernelResolver");

            this.kernelResolver = kernelResolver;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");

            var kernel = kernelResolver.Resolve(controllerType);
            return (IController)kernel.Get(controllerType);
        }
    }
}
