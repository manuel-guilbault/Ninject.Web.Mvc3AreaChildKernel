using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Xunit;

namespace Ninject.Web.MvcAreaChildKernel.Test
{
    public class DefaultAreaNamespaceMapCacheTest
    {
        private readonly Cache cache = HttpRuntime.Cache;

        private void ClearCache()
        {
            var enumerator = cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                cache.Remove((string)enumerator.Key);
            }
        }

        [Fact]
        public void TestMapAndResolve()
        {
            var @namespace = "Some.Namespace";
            var areaName = "TestArea";

            var sut = new DefaultAreaNamespaceMapCache(cache);

            sut.Map(@namespace, areaName);

            var result = sut.Resolve(@namespace);

            Assert.Equal(result, areaName);
            ClearCache();
        }

        [Fact]
        public void TestIgnoreAndIsIgnored()
        {
            var @namespace = "Some.Namespace";

            var sut = new DefaultAreaNamespaceMapCache(cache);

            sut.Ignore(@namespace);

            var result = sut.IsIgnored(@namespace);

            Assert.Equal(result, true);
            ClearCache();
        }

        [Fact]
        public void TestMapClearAndCannotResolve()
        {
            var @namespace = "Some.Namespace";
            var areaName = "TestArea";

            var sut = new DefaultAreaNamespaceMapCache(cache);

            sut.Map(@namespace, areaName);
            sut.Clear();

            var result = sut.Resolve(@namespace);

            Assert.Equal(result, null);
        }

        [Fact]
        public void TestIgnoreClearAndIsNotIgnored()
        {
            var @namespace = "Some.Namespace";

            var sut = new DefaultAreaNamespaceMapCache(cache);

            sut.Ignore(@namespace);
            sut.Clear();

            var result = sut.IsIgnored(@namespace);

            Assert.Equal(result, false);
        }
    }
}
