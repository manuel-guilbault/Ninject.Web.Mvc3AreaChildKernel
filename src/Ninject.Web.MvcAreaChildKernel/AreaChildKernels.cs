using Ninject.Web.MvcAreaChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public static class AreaChildKernels
    {
        readonly static ISet<string> names = new HashSet<string>();
        readonly static IDictionary<string, AreaChildKernel> namespaceMap = new Dictionary<string, AreaChildKernel>();

        readonly static ISet<string> outOfScopeNamespaceCache = new HashSet<string>();
        readonly static IDictionary<string, AreaChildKernel> namespaceMapCache = new Dictionary<string, AreaChildKernel>();

        public static void Register(AreaChildKernel areaChildKernel, params string[] namespaces)
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

        private static AreaChildKernel GetFromCache(string @namespace)
        {
            AreaChildKernel areaChildKernel;
            return namespaceMapCache.TryGetValue(@namespace, out areaChildKernel)
                ? areaChildKernel
                : null;
        }

        private static void AddToCache(string @namespace, AreaChildKernel areaChildKernel)
        {
            namespaceMapCache.Add(@namespace, areaChildKernel);
        }

        private static void ClearCache()
        {
            outOfScopeNamespaceCache.Clear();
            namespaceMapCache.Clear();
        }

        private static AreaChildKernel ResolveFromNamespace(string @namespace)
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

        private static bool IsIgnored(string @namespace)
        {
            return outOfScopeNamespaceCache.Contains(@namespace);
        }

        private static void Ignore(string @namespace)
        {
            outOfScopeNamespaceCache.Add(@namespace);
        }

        public static AreaChildKernel GetAreaChildKernel(Type type)
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
