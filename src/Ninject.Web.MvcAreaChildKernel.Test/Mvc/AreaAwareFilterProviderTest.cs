using Moq;
using Ninject.Web.Mvc.Filter;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test.Mvc
{
    public class AreaAwareFilterProviderTest
    {
        [Fact]
        public void TestGetFilters()
        {
            var controller = new Mock<ControllerBase>();

            var filter = new Filter(new object(), FilterScope.Global, null);

            var ninjectFilter = new Mock<INinjectFilter>();
            ninjectFilter.Setup(f => f.BuildFilter(It.IsAny<FilterContextParameter>())).Returns(filter);

            var kernel = new StandardKernel();
            kernel.Bind<INinjectFilter>().ToMethod(c => ninjectFilter.Object);

            var kernelResolve = new Mock<IKernelResolver>();
            kernelResolve.Setup(kr => kr.Resolve(controller.Object.GetType().Namespace)).Returns(kernel);

            var controllerContext = new ControllerContext(
                new System.Web.Routing.RequestContext(),
                controller.Object
            );
            var actionDescriptor = new Mock<ActionDescriptor>();

            var sut = new AreaAwareFilterProvider(kernelResolve.Object);

            var result = sut.GetFilters(controllerContext, actionDescriptor.Object);

            Assert.Equal(result.Single(), filter);
        }
    }
}
