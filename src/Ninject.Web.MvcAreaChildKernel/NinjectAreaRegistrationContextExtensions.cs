using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Web.MvcAreaChildKernel;
using System.Linq;

namespace System.Web.Mvc
{
    public static class NinjectAreaRegistrationContextExtensions
    {
        public static void UseKernel(this AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory)
        {
            var areaChildKernelRegistrar = DependencyResolver.Current.GetService<IAreaChildKernelRegistrar>();
            areaChildKernelRegistrar.Register(areaRegistrationContext, childKernelFactory);
        }
    }
}
