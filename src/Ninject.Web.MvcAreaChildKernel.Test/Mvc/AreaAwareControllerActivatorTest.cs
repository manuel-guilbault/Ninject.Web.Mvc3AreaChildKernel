using Moq;
using Ninject.Activation;
using Ninject.Parameters;
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
    public class AreaAwareControllerActivatorTest
    {
        [Fact]
        public void TestCreate()
        {
            var controller = new Mock<IController>();

            var kernel = new StandardKernel();
            kernel.Bind(controller.Object.GetType()).ToMethod(c => controller.Object);

            var kernelResolver = new Mock<IKernelResolver>();
            kernelResolver.Setup(kr => kr.Resolve(controller.Object.GetType().Namespace)).Returns(kernel);

            var sut = new AreaAwareControllerActivator(kernelResolver.Object);

            var result = sut.Create(new RequestContext(), controller.Object.GetType());

            Assert.Equal(controller.Object, result);
        }
    }
}
