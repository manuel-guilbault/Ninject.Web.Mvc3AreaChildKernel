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
        readonly IKernel kernel;
        
        public AreaAwareViewPageActivator(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.kernel = kernel;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            var effectiveKernel = kernel.ResolveChildKernelFor(controllerContext.Controller.GetType());
            return effectiveKernel.Get(type);
        }
    }
}
