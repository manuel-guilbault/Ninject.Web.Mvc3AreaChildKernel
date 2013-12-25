using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    /// <summary>
    /// Service used to resolve the kernel used for a given namespace.
    /// </summary>
    public interface IKernelResolver
    {
        /// <summary>
        /// Resolve the kernel to use for a given namespace.
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        /// <remarks>This method should never return null. It should be able to provide a default kernel
        /// if the namespace cannot be resolved.</remarks>
        /// <exception cref="ArgumentNullException">If namespace is null.</exception>
        IKernel Resolve(string @namespace);
    }
}
