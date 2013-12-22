using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class AreaChildKernelCollection : IAreaChildKernelCollection
    {
        readonly ISet<string> names = new HashSet<string>();
        readonly IDictionary<string, AreaChildKernel> namespaceMap = new Dictionary<string, AreaChildKernel>();

        readonly ISet<string> ignoredNamespaceCache = new HashSet<string>();
        readonly IDictionary<string, AreaChildKernel> namespaceMapCache = new Dictionary<string, AreaChildKernel>();

        private AreaChildKernel GetFromCache(string @namespace)
        {
            AreaChildKernel areaChildKernel;
            return namespaceMapCache.TryGetValue(@namespace, out areaChildKernel)
                ? areaChildKernel
                : null;
        }

        private void AddToCache(string @namespace, AreaChildKernel areaChildKernel)
        {
            namespaceMapCache.Add(@namespace, areaChildKernel);
        }

        private void ClearCache()
        {
            ignoredNamespaceCache.Clear();
            namespaceMapCache.Clear();
        }

        private AreaChildKernel ResolveFromNamespace(string @namespace)
        {
            AreaChildKernel areaChildKernel;

            var parts = @namespace.Split('.');
            var index = parts.Length;
            while (index > 0)
            {
                var @matchedNamespace = string.Join(".", parts.Take(index));
                if (namespaceMap.TryGetValue(@matchedNamespace, out areaChildKernel))
                {
                    AddToCache(@namespace, areaChildKernel);
                    return areaChildKernel;
                }

                --index;
            }
            return null;
        }

        private bool IsIgnored(string @namespace)
        {
            return ignoredNamespaceCache.Contains(@namespace);
        }

        private void Ignore(string @namespace)
        {
            ignoredNamespaceCache.Add(@namespace);
        }

        public void Register(AreaChildKernel areaChildKernel, params string[] namespaces)
        {
            if (areaChildKernel == null) throw new ArgumentNullException("areaChildKernel");
            if (names.Contains(areaChildKernel.Name)) throw new ArgumentException(string.Format("Area '{0}' already registered", areaChildKernel.Name), "areaChildKernel");
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            if (!namespaces.Any()) throw new ArgumentException("namespaces cannot be empty", "namespaces");

            foreach (var @namespace in namespaces)
            {
                if (namespaceMap.ContainsKey(@namespace))
                {
                    throw new ArgumentException(string.Format("Area already registered for namespace '{0}'"), "namespaces");
                }
            }

            names.Add(areaChildKernel.Name);
            foreach (var @namespace in namespaces)
            {
                namespaceMap.Add(@namespace, areaChildKernel);
            }

            ClearCache();
        }

        public AreaChildKernel GetFor(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (!namespaceMap.Any() || IsIgnored(type.Namespace))
            {
                return null;
            }

            var areaChildKernel = GetFromCache(type.Namespace) ?? ResolveFromNamespace(type.Namespace);
            if (areaChildKernel == null)
            {
                Ignore(type.Namespace);
            }

            return areaChildKernel;
        }
    }
}
