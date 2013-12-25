using Ninject.Web.Mvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    /// <summary>
    /// Filter provider that relies on a IKernelResolver to get the IKernel instance used to get 
    /// the filters based on the controller context.
    /// </summary>
    public class AreaAwareFilterProvider : IFilterProvider
    {
        private readonly IKernelResolver kernelResolver;

        public AreaAwareFilterProvider(IKernelResolver kernelResolver)
        {
            if (kernelResolver == null) throw new ArgumentNullException("kernelResolver");

            this.kernelResolver = kernelResolver;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");

            var parameter = new FilterContextParameter(controllerContext, actionDescriptor);
            var kernel = kernelResolver.Resolve(controllerContext.Controller.GetType().Namespace);
            var filters = kernel.GetAll<INinjectFilter>(parameter);
            return filters.Select(f => f.BuildFilter(parameter));
        }
    }
}
