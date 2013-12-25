using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class DefaultAreaNamespaceMapCache : IAreaNamespaceMapCache
    {
        //private readonly ISet<string> ignoredNamespaces = new HashSet<string>();
        //private readonly IDictionary<string, string> namespaceToAreaNameMap = new Dictionary<string, string>();

        //public string Resolve(string @namespace)
        //{
        //    if (@namespace == null) throw new ArgumentNullException("namespace");

        //    string areaName;
        //    return namespaceToAreaNameMap.TryGetValue(@namespace, out areaName)
        //        ? areaName
        //        : null;
        //}

        //public void Map(string @namespace, string areaName)
        //{
        //    if (@namespace == null) throw new ArgumentNullException("namespace");
        //    if (areaName == null) throw new ArgumentNullException("areaName");

        //    namespaceToAreaNameMap.Add(@namespace, areaName);
        //}

        //public bool IsIgnored(string @namespace)
        //{
        //    if (@namespace == null) throw new ArgumentNullException("namespace");

        //    return ignoredNamespaces.Contains(@namespace);
        //}

        //public void Ignore(string @namespace)
        //{
        //    if (@namespace == null) throw new ArgumentNullException("namespace");

        //    ignoredNamespaces.Add(@namespace);
        //}

        //public void Clear()
        //{
        //    namespaceToAreaNameMap.Clear();
        //    ignoredNamespaces.Clear();
        //}

        private const string clearDependencyKey = "Ninject.Web.MvcAreaChildKernel.ClearTrigger";
        private readonly static object ignoreValue = new object();

        private readonly Cache cache;

        public DefaultAreaNamespaceMapCache(Cache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");

            this.cache = cache;
        }

        private CacheDependency GetClearDependency()
        {
            if (cache.Get(clearDependencyKey) == null)
            {
                cache.Add(clearDependencyKey, true, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            }
            return new CacheDependency(null, new[] { clearDependencyKey });
        }

        private string GetMapCacheKey(string @namespace)
        {
            return "Ninject.Web.MvcAreaChildKernel.AreaNameFor:" + @namespace;
        }

        private string GetIgnoredCacheKey(string @namespace)
        {
            return "Ninject.Web.MvcAreaChildKernel.Ignore:" + @namespace;
        }

        public string Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            return (string)cache.Get(GetMapCacheKey(@namespace));
        }

        public void Map(string @namespace, string areaName)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");
            if (areaName == null) throw new ArgumentNullException("areaName");

            cache.Add(
                GetMapCacheKey(@namespace),
                areaName,
                GetClearDependency(),
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default,
                null
            );
        }

        public bool IsIgnored(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            return Object.ReferenceEquals(
                cache.Get(GetIgnoredCacheKey(@namespace)),
                ignoreValue
            );
        }

        public void Ignore(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            cache.Add(
                GetIgnoredCacheKey(@namespace),
                ignoreValue,
                GetClearDependency(),
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default,
                null
            );
        }

        public void Clear()
        {
            cache.Remove(clearDependencyKey);
        }
    }
}
