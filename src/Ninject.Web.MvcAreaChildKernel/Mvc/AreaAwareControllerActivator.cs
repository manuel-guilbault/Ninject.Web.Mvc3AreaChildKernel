using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    /// <summary>
    /// Controller activator that relies on a IKernelResolver to get the IKernel instance used
    /// to create the controller.
    /// </summary>
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

            var kernel = kernelResolver.Resolve(controllerType.Namespace);
            return (IController)kernel.Get(controllerType);
        }
    }
}
