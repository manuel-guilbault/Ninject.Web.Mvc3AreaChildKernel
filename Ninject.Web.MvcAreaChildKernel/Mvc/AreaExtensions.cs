using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    static class AreaExtensions
    {
        public static IKernel ResolveChildKernelFor(this IKernel kernel, Type type)
        {
            var areaChildKernel = AreaChildKernels.GetAreaChildKernel(type);
            if (areaChildKernel == null)
            {
                return kernel;
            }

            return areaChildKernel.GetChildKernel(kernel);
        }
    }
}
