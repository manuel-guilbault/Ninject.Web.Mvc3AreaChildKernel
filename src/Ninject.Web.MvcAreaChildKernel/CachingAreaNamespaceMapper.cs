using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class CachingAreaNamespaceMapper : IAreaNamespaceMapper
    {
        private readonly IAreaNamespaceMapper mapper;
        private readonly IAreaNamespaceMapCache cache;

        public CachingAreaNamespaceMapper(IAreaNamespaceMapper mapper, IAreaNamespaceMapCache cache)
        {
            if (mapper == null) throw new ArgumentNullException("mapper");
            if (cache == null) throw new ArgumentNullException("cache");

            this.mapper = mapper;
            this.cache = cache;
        }

        private string ResolveFromMapper(string @namespace)
        {
            var areaName = mapper.Resolve(@namespace);
            if (areaName == null)
            {
                cache.Ignore(@namespace);
            }
            else
            {
                cache.Map(@namespace, areaName);
            }
            return areaName;
        }

        public void Register(string areaName, params string[] namespaces)
        {
            mapper.Register(areaName, namespaces);
            cache.Clear();
        }

        public string Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            if (cache.IsIgnored(@namespace))
            {
                return null;
            }

            return cache.Resolve(@namespace) ?? ResolveFromMapper(@namespace);
        }
    }
}
