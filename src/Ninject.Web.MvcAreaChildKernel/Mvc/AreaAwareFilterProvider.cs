using Ninject.Web.Mvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareFilterProvider : IFilterProvider
    {
        private readonly IAreaChildKernelCollection areaChildKernels;
        private readonly IKernel kernel;

        public AreaAwareFilterProvider(IAreaChildKernelCollection areaChildKernels, IKernel kernel)
        {
            if (areaChildKernels == null) throw new ArgumentNullException("areaChildKernels");
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.areaChildKernels = areaChildKernels;
            this.kernel = kernel;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var parameter = new FilterContextParameter(controllerContext, actionDescriptor);
            var effectiveKernel = areaChildKernels.ResolveChildKernel(kernel, controllerContext.Controller.GetType());
            var ninjectFilters = effectiveKernel.GetAll<INinjectFilter>(parameter);
            foreach (var filter in ninjectFilters)
            {
                yield return filter.BuildFilter(parameter);
            }
        }
    }
}
