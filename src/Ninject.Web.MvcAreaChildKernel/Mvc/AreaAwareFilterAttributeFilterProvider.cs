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
        private readonly IAreaChildKernelCollection areaChildKernels;
        private readonly IKernel kernel;

        public AreaAwareFilterAttributeFilterProvider(IAreaChildKernelCollection areaChildKernels, IKernel kernel)
        {
            if (areaChildKernels == null) throw new ArgumentNullException("areaChildKernels");
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.areaChildKernels = areaChildKernels;
            this.kernel = kernel;
        }

        protected IKernel ResolveChildKernel(ControllerContext controllerContext)
        {
            return areaChildKernels.ResolveChildKernel(kernel, controllerContext.Controller.GetType());
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            var effectiveKernel = ResolveChildKernel(controllerContext);
            foreach (var attribute in attributes)
            {
                effectiveKernel.Inject(attribute);
            }
            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            var effectiveKernel = ResolveChildKernel(controllerContext);
            foreach (var attribute in attributes)
            {
                effectiveKernel.Inject(attribute);
            }
            return attributes;
        }
    }
}
