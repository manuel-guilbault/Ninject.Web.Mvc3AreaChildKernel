using Ninject.Web.Mvc;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test
{
    public class MvcAreaChildKernelModuleTest
    {
        [Fact]
        public void TestServices()
        {
            var kernel = new StandardKernel(
                new NinjectSettings()
                {
                    LoadExtensions = false
                },
                new MvcModule(),
                new MvcAreaChildKernelModule()
            );

            Assert.IsType<AreaAwareControllerActivator>(kernel.Get<IControllerActivator>());
            Assert.IsType<AreaAwareViewPageActivator>(kernel.Get<IViewPageActivator>());

            var filterProviders = kernel.GetAll<IFilterProvider>();
            Assert.Equal(filterProviders.Any(fp => fp is AreaAwareFilterProvider), true);
            Assert.Equal(filterProviders.Any(fp => fp is AreaAwareFilterAttributeFilterProvider), true);
        }
    }
}
