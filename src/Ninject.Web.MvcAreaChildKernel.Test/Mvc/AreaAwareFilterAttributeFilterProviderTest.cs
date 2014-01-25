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
    public class AreaAwareFilterAttributeFilterProviderTest
    {
        [Fact]
        public void TestGetFilters()
        {
            var controller = new ControllerMock();

            var testService = new Mock<ITestService>();

            var kernel = new StandardKernel();
            kernel.Bind<ITestService>().ToMethod(c => testService.Object);

            var kernelResolver = new Mock<IKernelResolver>();
            kernelResolver.Setup(kr => kr.Resolve(controller.GetType().Namespace)).Returns(kernel);

            var sut = new AreaAwareFilterAttributeFilterProvider(kernelResolver.Object);

            var controllerContext = new ControllerContext(
                new RequestContext(),
                controller
            );
            var actionDescriptor = new ReflectedActionDescriptor(
                controller.GetType().GetMethod("Index"),
                "Index",
                new ReflectedControllerDescriptor(controller.GetType())
            );
            var result = sut.GetFilters(controllerContext, actionDescriptor);

            var controllerFilter = (TestFilterAttribute)result.Single(f => f.Scope == FilterScope.Controller).Instance;
            Assert.Equal(controllerFilter.TestService, testService.Object);

            var actionFilter = (TestFilterAttribute)result.Single(f => f.Scope == FilterScope.Action).Instance;
            Assert.Equal(actionFilter.TestService, testService.Object);
        }

        public interface ITestService
        {
        }

        public class TestFilterAttribute : FilterAttribute
        {
            [Inject]
            public ITestService TestService { get; set; }
        }

        [TestFilter]
        public class ControllerMock : Controller
        {
            [TestFilter]
            public ActionResult Index()
            {
                return null;
            }
        }
    }
}
