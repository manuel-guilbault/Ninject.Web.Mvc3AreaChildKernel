using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class DefaultAreaNamespaceMapper : IAreaNamespaceMapper
    {
        private readonly ISet<string> areaNames = new HashSet<string>();
        private readonly IDictionary<string, string> namespaceToAreaNameMap = new Dictionary<string, string>();

        private readonly IAreaNamespaceMapCache cache;

        public DefaultAreaNamespaceMapper(IAreaNamespaceMapCache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");

            this.cache = cache;
        }

        protected virtual string ResolveNamespace(string @namespace)
        {
            string areaName;

            var parts = @namespace.Split('.');
            var index = parts.Length;
            while (index > 0)
            {
                var @matchedNamespace = string.Join(".", parts.Take(index));
                if (namespaceToAreaNameMap.TryGetValue(@matchedNamespace, out areaName))
                {
                    cache.Map(@namespace, areaName);
                    return areaName;
                }

                --index;
            }
            return null;
        }

        public void Register(string areaName, params string[] namespaces)
        {
            if (areaName == null) throw new ArgumentNullException("areaName");
            if (areaNames.Contains(areaName)) throw new ArgumentException(string.Format("Area '{0}' already registered", areaName), "areaName");
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            if (!namespaces.Any()) throw new ArgumentException("namespaces cannot be empty", "namespaces");

            foreach (var @namespace in namespaces)
            {
                if (namespaceToAreaNameMap.ContainsKey(@namespace))
                {
                    throw new ArgumentException(string.Format("Area already registered for namespace '{0}'", @namespace), "namespaces");
                }
            }

            areaNames.Add(areaName);
            foreach (var @namespace in namespaces)
            {
                namespaceToAreaNameMap.Add(@namespace, areaName);
            }

            cache.Clear();
        }

        public string Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            if (cache.IsIgnored(@namespace))
            {
                return null;
            }

            var areaName = cache.Resolve(@namespace) ?? ResolveNamespace(@namespace);
            if (areaName == null)
            {
                cache.Ignore(@namespace);
            }

            return areaName;
        }
    }
}
