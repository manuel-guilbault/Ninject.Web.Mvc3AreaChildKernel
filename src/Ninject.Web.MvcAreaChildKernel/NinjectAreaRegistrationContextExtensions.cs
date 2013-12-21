using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Web.MvcAreaChildKernel;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public static class NinjectAreaRegistrationContextExtensions
    {
        public static void UseKernel(this AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory)
        {
            if (childKernelFactory == null) throw new ArgumentNullException("childKernelFactory");

            AreaChildKernels.Register(
                new AreaChildKernel(areaRegistrationContext.AreaName, childKernelFactory), 
                areaRegistrationContext.Namespaces.Select(ns => ns.TrimEnd('.', '*')).ToArray()
            );
        }
    }
}
