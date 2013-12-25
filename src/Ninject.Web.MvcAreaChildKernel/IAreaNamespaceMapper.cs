using Ninject.Extensions.ChildKernel;
using System;

namespace Ninject.Web.MvcAreaChildKernel
{
    /// <summary>
    /// A service that handles mapping between area names and namespaces.
    /// </summary>
    public interface IAreaNamespaceMapper
    {
        /// <summary>
        /// Register a set of namespaces for an area name.
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="namespaces"></param>
        /// <exception cref="ArgumentNullException">If areaName is null -or- if namespaces is null.</exception>
        /// <exception cref="ArgumentException">If areaName has already been registered -or- if namespaces is empty.</exception>
        void Register(string areaName, params string[] namespaces);

        /// <summary>
        /// Resolve the area name for a given namespace.
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns>The area name, or null if the namespace cannot be mapped to an area.</returns>
        /// <exception cref="ArgumentNullException">If namespace is null.</exception>
        string Resolve(string @namespace);
    }
}
