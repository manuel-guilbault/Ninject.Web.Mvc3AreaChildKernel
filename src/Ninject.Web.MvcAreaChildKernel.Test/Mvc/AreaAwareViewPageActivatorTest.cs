using Moq;
using Ninject.Web.MvcAreaChildKernel.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test.Mvc
{
    public class AreaAwareViewPageActivatorTest
    {
        [Fact]
        public void TestCreate()
        {
            var controller = new Mock<ControllerBase>();
            var view = new Mock<System.Web.Mvc.WebViewPage>();

            var kernel = new StandardKernel();
            kernel.Bind(view.Object.GetType()).ToMethod(c => view.Object);

            var kernelResolver = new Mock<IKernelResolver>();
            kernelResolver.Setup(kr => kr.Resolve(controller.Object.GetType().Namespace)).Returns(kernel);

            var controllerContext = new ControllerContext(
                new RequestContext(),
                controller.Object
            );

            var sut = new AreaAwareViewPageActivator(kernelResolver.Object);

            var result = sut.Create(controllerContext, view.Object.GetType());

            Assert.Equal(result, view.Object);
        }
    }
}
