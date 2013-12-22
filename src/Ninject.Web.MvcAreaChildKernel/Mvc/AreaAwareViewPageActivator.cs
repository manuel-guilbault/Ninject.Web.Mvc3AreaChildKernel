using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var effectiveKernel = areaChildKernels.ResolveChildKernel(kernel, controllerContext.Controller.GetType());
            return effectiveKernel.Get(type);
        }
    }
}
