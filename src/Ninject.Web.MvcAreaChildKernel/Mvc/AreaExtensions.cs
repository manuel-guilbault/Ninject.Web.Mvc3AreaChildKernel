using System;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    static class AreaExtensions
    {
        public static IKernel ResolveChildKernel(this IKernel kernel, Type type)
        {
            return AreaChildKernels.Collection.ResolveChildKernel(kernel, type);
        }

        public static IKernel ResolveChildKernel(this IAreaChildKernelCollection areaChildKernels, IKernel kernel, Type type)
        {
            if (areaChildKernels == null) throw new ArgumentNullException("areaChildKernels");
            if (type == null) throw new ArgumentNullException("type");

            var areaChildKernel = areaChildKernels.GetFor(type);
            if (areaChildKernel == null)
            {
                return kernel;
            }

            return areaChildKernel.GetChildKernel(kernel);
        }
    }
}
