using Moq;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test
{
    public class NinjectAreaRegistrationContextExtensionsTest
    {
        [Fact]
        public void TestUseKernel()
        {
            var context = new AreaRegistrationContext("TestArea", RouteTable.Routes);
            Func<IKernel, IChildKernel> factory = c => null;

            var areaChildKernelRegistrar = new Mock<IAreaChildKernelRegistrar>();
            areaChildKernelRegistrar.Setup(r => r.Register(context, factory)).Verifiable();

            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup(dr => dr.GetService(typeof(IAreaChildKernelRegistrar))).Returns(areaChildKernelRegistrar.Object);

            var currentDependencyResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(dependencyResolver.Object);

            NinjectAreaRegistrationContextExtensions.UseKernel(context, factory);

            areaChildKernelRegistrar.Verify(r => r.Register(context, factory), Times.Once);
            DependencyResolver.SetResolver(currentDependencyResolver);
        }
    }
}
