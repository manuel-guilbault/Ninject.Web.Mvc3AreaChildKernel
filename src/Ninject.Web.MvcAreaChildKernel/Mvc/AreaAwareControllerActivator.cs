using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareControllerActivator : IControllerActivator
    {
        private readonly IAreaChildKernelCollection areaChildKernels;
        private readonly IKernel kernel;

        public AreaAwareControllerActivator(IAreaChildKernelCollection areaChildKernels, IKernel kernel)
        {
            if (areaChildKernels == null) throw new ArgumentNullException("areaChildKernels");
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.areaChildKernels = areaChildKernels;
            this.kernel = kernel;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");

            return (IController)areaChildKernels.ResolveChildKernel(kernel, controllerType).Get(controllerType);
        }
    }
}
