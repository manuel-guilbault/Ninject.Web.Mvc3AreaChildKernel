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

        public void Register(string areaName, params string[] namespaces)
        {
            if (areaName == null) throw new ArgumentNullException("areaName");
            if (areaNames.Contains(areaName)) throw new ArgumentException(string.Format("Area '{0}' already registered", areaName), "areaName");
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            if (!namespaces.Any()) throw new ArgumentException("namespaces cannot be empty", "namespaces");

            var existingNamespace = namespaces.FirstOrDefault(ns => namespaceToAreaNameMap.ContainsKey(ns));
            if (existingNamespace != null) throw new ArgumentException(string.Format("Area already registered for namespace '{0}'", existingNamespace), "namespaces");

            areaNames.Add(areaName);
            foreach (var @namespace in namespaces)
            {
                namespaceToAreaNameMap.Add(@namespace, areaName);
            }
        }

        public string Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            string areaName;

            // Try to directly resolve the namespace.
            if (namespaceToAreaNameMap.TryGetValue(@namespace, out areaName))
            {
                return areaName;
            }

            // Walk the parent namespaces and try to resolve the area name using the wildcard pattern.
            // For example, if @namespace is "MvcSample.Areas.Admin.Controllers" and a map entry
            // exists for "MvcSample.Areas.Admin.*", it will be matched.
            var parts = @namespace.Split('.');
            var patternLength = parts.Length;
            while (patternLength > 0)
            {
                var patternParts = parts.Take(patternLength).Concat(new[] { "*" });
                var pattern = string.Join(".", patternParts);
                if (namespaceToAreaNameMap.TryGetValue(pattern, out areaName))
                {
                    return areaName;
                }

                --patternLength;
            }

            // Cannot resolve the namespace.
            return null;
        }
    }
}
