using Ninject.Web.MvcAreaChildKernel.Mvc;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class MvcAreaChildKernelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IControllerActivator>().To<AreaAwareControllerActivator>();

            //Unbind<IFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterProvider>();
            Bind<IFilterProvider>().To<AreaAwareFilterAttributeFilterProvider>();

            Bind<IViewPageActivator>().To<AreaAwareViewPageActivator>();
        }
    }
}
