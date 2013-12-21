using Ninject.Web.Mvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        readonly IKernel kernel;

        public AreaAwareFilterAttributeFilterProvider(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.kernel = kernel;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            var effectiveKernel = kernel.ResolveChildKernelFor(controllerContext.Controller.GetType());
            foreach (var attribute in attributes)
            {
                effectiveKernel.Inject(attribute);
            }
            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            var effectiveKernel = kernel.ResolveChildKernelFor(controllerContext.Controller.GetType());
            foreach (var attribute in attributes)
            {
                effectiveKernel.Inject(attribute);
            }
            return attributes;
        }
    }
}
