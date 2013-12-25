using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Web.MvcAreaChildKernel;
using System.Linq;

namespace System.Web.Mvc
{
    public static class NinjectAreaRegistrationContextExtensions
    {
        /// <summary>
        /// Bind an area to a child kernel.
        /// </summary>
        /// <param name="areaRegistrationContext"></param>
        /// <param name="childKernelFactory"></param>
        /// <remarks>This extension method uses the current IDependencyResolver to get the
        /// IAreaChildKernelRegistrar implementation, which is used to register the area's child kernel.</remarks>
        /// <exception cref="ArgumentNullException">If areaRegistrationContext is null -or- if childKernelFactory is null.</exception>
        public static void UseKernel(this AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory)
        {
            if (areaRegistrationContext == null) throw new ArgumentNullException("areaRegistrationContext");
            if (childKernelFactory == null) throw new ArgumentNullException("childKernelFactory");

            var areaChildKernelRegistrar = DependencyResolver.Current.GetService<IAreaChildKernelRegistrar>();
            areaChildKernelRegistrar.Register(areaRegistrationContext, childKernelFactory);
        }
    }
}
