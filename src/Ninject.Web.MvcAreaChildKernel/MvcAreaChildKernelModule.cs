using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class MvcAreaChildKernelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAreaChildKernelRegistrar, IKernelResolver>().To<DefaultAreaChildKernelRegistry>().InRequestScope();
            Bind<IAreaNamespaceMapper>().To<CachingAreaNamespaceMapper>().InRequestScope();
            Bind<IAreaNamespaceMapper>().To<DefaultAreaNamespaceMapper>()
                .WhenInjectedExactlyInto<CachingAreaNamespaceMapper>()
                .InSingletonScope();
            Bind<IAreaNamespaceMapCache>().To<DefaultAreaNamespaceMapCache>().InRequestScope();
            Bind<Cache>().ToMethod(c => HttpContext.Current.Cache);

            BindMvcServices();
        }

        protected virtual void BindMvcServices()
        {
            Bind<IControllerActivator>().To<AreaAwareControllerActivator>();

            Unbind<IFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterAttributeFilterProvider>();

            Bind<IViewPageActivator>().To<AreaAwareViewPageActivator>();
        }
    }
}
