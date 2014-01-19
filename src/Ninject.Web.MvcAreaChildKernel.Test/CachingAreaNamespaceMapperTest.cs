using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test
{
    public class CachingAreaNamespaceMapperTest
    {
        [Fact]
        public void TestRegister()
        {
            var areaName = "MyArea";
            var namespaces = new[] { "Some.Namespace" };

            var mapper = new Mock<IAreaNamespaceMapper>();
            mapper.Setup(m => m.Register(areaName, namespaces)).Verifiable();

            var cache = new Mock<IAreaNamespaceMapCache>();
            cache.Setup(c => c.Clear()).Verifiable();

            var sut = new CachingAreaNamespaceMapper(mapper.Object, cache.Object);
            sut.Register(areaName, namespaces);

            mapper.Verify(m => m.Register(areaName, namespaces), Times.Once);
            cache.Verify(c => c.Clear(), Times.Once);
        }

        [Fact]
        public void TestResolveIgnored()
        {
            var @namespace = "Some.Namespace";

            var mapper = new Mock<IAreaNamespaceMapper>();

            var cache = new Mock<IAreaNamespaceMapCache>();
            cache.Setup(c => c.IsIgnored(@namespace)).Returns(true);

            var sut = new CachingAreaNamespaceMapper(mapper.Object, cache.Object);
            var result = sut.Resolve(@namespace);

            Assert.Null(result);
        }

        [Fact]
        public void TestResolveFromMapper()
        {
            var areaName = "MyArea";
            var @namespace = "Some.Namespace";

            var mapper = new Mock<IAreaNamespaceMapper>();
            mapper.Setup(m => m.Resolve(@namespace)).Returns(areaName);

            var cache = new Mock<IAreaNamespaceMapCache>();
            cache.Setup(c => c.IsIgnored(It.IsAny<string>())).Returns(false);
            cache.Setup(c => c.Resolve(It.IsAny<string>())).Returns<string>(null);

            var sut = new CachingAreaNamespaceMapper(mapper.Object, cache.Object);
            var result = sut.Resolve(@namespace);

            Assert.Equal(result, areaName);
        }

        [Fact]
        public void TestResolveFromCache()
        {
            var areaName = "MyArea";
            var @namespace = "Some.Namespace";

            var mapper = new Mock<IAreaNamespaceMapper>();

            var cache = new Mock<IAreaNamespaceMapCache>();
            cache.Setup(c => c.IsIgnored(It.IsAny<string>())).Returns(false);
            cache.Setup(c => c.Resolve(@namespace)).Returns(areaName);

            var sut = new CachingAreaNamespaceMapper(mapper.Object, cache.Object);
            var result = sut.Resolve(@namespace);

            Assert.Equal(result, areaName);
        }
    }
}
