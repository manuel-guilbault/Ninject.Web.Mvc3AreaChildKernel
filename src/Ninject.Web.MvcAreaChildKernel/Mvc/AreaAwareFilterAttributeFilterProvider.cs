using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IKernelResolver kernelResolver;

        public AreaAwareFilterAttributeFilterProvider(IKernelResolver kernelResolver)
        {
            if (kernelResolver == null) throw new ArgumentNullException("kernelResolver");

            this.kernelResolver = kernelResolver;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            var kernel = kernelResolver.Resolve(controllerContext.Controller.GetType());
            foreach (var attribute in attributes)
            {
                kernel.Inject(attribute);
            }
            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            var kernel = kernelResolver.Resolve(controllerContext.Controller.GetType());
            foreach (var attribute in attributes)
            {
                kernel.Inject(attribute);
            }
            return attributes;
        }
    }
}
