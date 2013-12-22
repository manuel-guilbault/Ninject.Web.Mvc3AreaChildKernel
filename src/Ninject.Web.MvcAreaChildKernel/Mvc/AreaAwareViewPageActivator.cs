using System;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareViewPageActivator : IViewPageActivator
    {
        private readonly IAreaChildKernelCollection areaChildKernels;
        private readonly IKernel kernel;
        
        public AreaAwareViewPageActivator(IAreaChildKernelCollection areaChildKernels, IKernel kernel)
        {
            if (areaChildKernels == null) throw new ArgumentNullException("areaChildKernels");
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.areaChildKernels = areaChildKernels;
            this.kernel = kernel;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");
            if (type == null) throw new ArgumentNullException("type");

            var effectiveKernel = areaChildKernels.ResolveChildKernel(kernel, controllerContext.Controller.GetType());
            return effectiveKernel.Get(type);
        }
    }
}
