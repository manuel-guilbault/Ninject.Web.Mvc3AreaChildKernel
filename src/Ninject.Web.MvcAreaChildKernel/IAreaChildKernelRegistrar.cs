using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    /// <summary>
    /// A registrar to map an AreaRegistrationContext to a child kernel.
    /// </summary>
    public interface IAreaChildKernelRegistrar
    {
        /// <summary>
        /// Register a child kernel to an AreaRegistrationContext.
        /// </summary>
        /// <param name="areaRegistrationContext"></param>
        /// <param name="childKernelFactory"></param>
        /// <exception cref="ArgumentNullException">If areaRegistrationContext is null -or- if childKernelFactory is null.</exception>
        void Register(AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory);
    }
}
