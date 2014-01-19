using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test
{
    public class DefaultAreaNamespaceMapperTest
    {
        [Fact]
        public void TestCannotResolveUnknownNamespace()
        {
            var sut = new DefaultAreaNamespaceMapper();
            sut.Register("MyArea", "Some.Namespace");

            var result = sut.Resolve("Unknown.Namespace");

            Assert.Null(result);
        }

        [Fact]
        public void TestResolveDirectNamespace()
        {
            var sut = new DefaultAreaNamespaceMapper();
            sut.Register("MyArea", "Some.Namespace");

            var result = sut.Resolve("Some.Namespace");

            Assert.Equal(result, "MyArea");
        }

        [Fact]
        public void TestResolveDirectWildcardNamespace()
        {
            var sut = new DefaultAreaNamespaceMapper();
            sut.Register("MyArea", "Some.Namespace.*");

            var result = sut.Resolve("Some.Namespace");

            Assert.Equal(result, "MyArea");
        }

        [Fact]
        public void TestResolveChildWildcardNamespace()
        {
            var sut = new DefaultAreaNamespaceMapper();
            sut.Register("MyArea", "Some.Namespace.*");

            var result = sut.Resolve("Some.Namespace.Child");

            Assert.Equal(result, "MyArea");
        }
    }
}
