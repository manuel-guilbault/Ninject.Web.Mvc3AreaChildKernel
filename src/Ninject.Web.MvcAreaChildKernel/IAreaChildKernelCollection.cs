using System;

namespace Ninject.Web.MvcAreaChildKernel
{
    public interface IAreaChildKernelCollection
    {
        /// <summary>
        /// Register an area child kernel for a given set of namespaces.
        /// </summary>
        /// <param name="areaChildKernel"></param>
        /// <param name="namespaces"></param>
        /// <exception cref="ArgumentNullException">If areaChildKernel is null -or- if namespaces is null.</exception>
        /// <exception cref="ArgumentException">If an area child kernel with the same area name has already been registered -or- if namespaces is empty.</exception>
        void Register(AreaChildKernel areaChildKernel, params string[] namespaces);

        /// <summary>
        /// Get the area child kernel for a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The area child kernel, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">If type is null.</exception>
        AreaChildKernel GetFor(Type type);
    }
}
