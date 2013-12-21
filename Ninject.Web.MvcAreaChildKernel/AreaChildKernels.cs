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
        readonly static IDictionary<string, AreaChildKernel> nameMap = new Dictionary<string, AreaChildKernel>();
        readonly static IDictionary<string, AreaChildKernel> namespaceMap = new Dictionary<string, AreaChildKernel>();

        public static void Register(AreaChildKernel areaChildKernel, params string[] namespaces)
        {
            if (areaChildKernel == null) throw new ArgumentNullException("areaChildKernel");
            if (nameMap.ContainsKey(areaChildKernel.Name)) throw new ArgumentException(string.Format("Area '{0}' already registered", areaChildKernel.Name), "areaChildKernel");
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            if (!namespaces.Any()) throw new ArgumentException("namespaces cannot be empty", "namespaces");

            foreach (var @namespace in namespaces)
            {
                if (namespaceMap.ContainsKey(@namespace))
                {
                    throw new ArgumentException(string.Format("Area already registered for namespace '{0}'"), "namespaces");
                }
            }

            nameMap.Add(areaChildKernel.Name, areaChildKernel);
            foreach (var @namespace in namespaces)
            {
                namespaceMap.Add(@namespace, areaChildKernel);
            }
        }

        public static AreaChildKernel GetAreaChildKernel(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (!namespaceMap.Any())
            {
                return null;
            }

            AreaChildKernel areaChildKernel;

            var parts = type.Namespace.Split('.');
            var index = parts.Length;
            while (index > 0)
            {
                var @namespace = string.Join(".", parts.Take(index));
                if (namespaceMap.TryGetValue(@namespace, out areaChildKernel))
                {
                    return areaChildKernel;
                }

                --index;
            }
            return null;
        }
    }
}
