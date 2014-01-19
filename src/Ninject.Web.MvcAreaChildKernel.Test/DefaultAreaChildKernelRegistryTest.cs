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
    public class DefaultAreaChildKernelRegistryTest
    {
        [Fact]
        public void TestRegister()
        {
            var areaName = "MyArea";
            var namespaces = new[] { "Some.Namespace.*" };

            var kernel = new StandardKernel();
            var childKernel = new ChildKernel(kernel);

            var mapper = new Mock<IAreaNamespaceMapper>();
            mapper.Setup(m => m.Register(areaName, namespaces)).Verifiable();

            var areaContext = new AreaRegistrationContext(areaName, new RouteCollection());
            foreach (var ns in namespaces)
            {
                areaContext.Namespaces.Add(ns);
            }

            var sut = new DefaultAreaChildKernelRegistry(kernel, mapper.Object);
            sut.Register(areaContext, k => childKernel);

            mapper.Verify(m => m.Register(areaName, namespaces), Times.Once);
            Assert.Equal(childKernel, kernel.Get<IChildKernel>("Area:" + areaName));
        }

        [Fact]
        public void TestResolveUnknownNamespace()
        {
            var kernel = new StandardKernel();

            var mapper = new Mock<IAreaNamespaceMapper>();
            mapper.Setup(m => m.Resolve(It.IsAny<string>())).Returns<string>(null);

            var sut = new DefaultAreaChildKernelRegistry(kernel, mapper.Object);
            var result = sut.Resolve("Some.Namespace");

            Assert.Equal(result, kernel);
        }

        [Fact]
        public void TestResolveNamespace()
        {
            var areaName = "MyArea";
            var @namespace = "Some.Namespace";

            var kernel = new StandardKernel();
            var childKernel = new ChildKernel(kernel);
            kernel.Bind<IChildKernel>().ToConstant(childKernel).Named("Area:" + areaName);

            var mapper = new Mock<IAreaNamespaceMapper>();
            mapper.Setup(m => m.Resolve(@namespace)).Returns(areaName);

            var sut = new DefaultAreaChildKernelRegistry(kernel, mapper.Object);
            var result = sut.Resolve(@namespace);

            Assert.Equal(result, childKernel);
        }
    }
}
