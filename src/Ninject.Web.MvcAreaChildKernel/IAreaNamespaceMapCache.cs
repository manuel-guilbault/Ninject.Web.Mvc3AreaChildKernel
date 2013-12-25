using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    /// <summary>
    /// A cache service for computed namespace to area names mapping.
    /// </summary>
    public interface IAreaNamespaceMapCache
    {
        /// <summary>
        /// Resolve the area name for a given namespace.
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns>The area name, or null if none found.</returns>
        /// <exception cref="ArgumentNullException">If namespace is null.</exception>
        string Resolve(string @namespace);

        /// <summary>
        /// Add mapping between a namespace and an area name.
        /// </summary>
        /// <param name="namespace"></param>
        /// <param name="areaName"></param>
        /// <exception cref="ArgumentNullException">If namespace is null -or- if areaName is null.</exception>
        void Map(string @namespace, string areaName);

        /// <summary>
        /// Check is a given namespace should be ignored for map checking.
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If namespace is null.</exception>
        bool IsIgnored(string @namespace);

        /// <summary>
        /// Mark a namespace to be ignored for map checking.
        /// </summary>
        /// <param name="namespace"></param>
        /// <exception cref="ArgumentNullException">If namespace is null.</exception>
        void Ignore(string @namespace);

        /// <summary>
        /// Clear all cached information.
        /// </summary>
        void Clear();
    }
}
