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
        readonly IKernel kernel;

        public AreaAwareFilterProvider(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.kernel = kernel;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var parameter = new FilterContextParameter(controllerContext, actionDescriptor);
            var effectiveKernel = kernel.ResolveChildKernelFor(controllerContext.Controller.GetType());
            var ninjectFilters = effectiveKernel.GetAll<INinjectFilter>(parameter);
            foreach (var filter in ninjectFilters)
            {
                yield return filter.BuildFilter(parameter);
            }
        }
    }
}
