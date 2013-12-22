using Ninject.Modules;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class MvcAreaChildKernelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAreaChildKernelCollection>().ToMethod(c => AreaChildKernels.Collection).InTransientScope();

            Bind<IControllerActivator>().To<AreaAwareControllerActivator>();

            Unbind<IFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterAttributeFilterProvider>();

            Bind<IViewPageActivator>().To<AreaAwareViewPageActivator>();
        }
    }
}
