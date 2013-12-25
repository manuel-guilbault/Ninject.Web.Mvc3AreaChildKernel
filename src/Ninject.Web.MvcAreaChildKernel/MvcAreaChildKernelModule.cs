using Ninject.Modules;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System.Web;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class MvcAreaChildKernelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAreaNamespaceMapper>().To<DefaultAreaNamespaceMapper>().InSingletonScope();
            Bind<IAreaNamespaceMapCache>().ToMethod(c => new DefaultAreaNamespaceMapCache(HttpContext.Current.Cache));
            Bind<IAreaChildKernelRegistrar, IKernelResolver>().To<DefaultAreaChildKernelRegistry>().InSingletonScope();

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
