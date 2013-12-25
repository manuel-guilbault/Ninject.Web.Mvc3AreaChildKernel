using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class DefaultAreaChildKernelRegistry : IAreaChildKernelRegistrar, IKernelResolver
    {
        private readonly IKernel kernel;
        private readonly IAreaNamespaceMapper areaNamespaceMapper;

        public DefaultAreaChildKernelRegistry(IKernel kernel, IAreaNamespaceMapper areaNamespaceMapper)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (areaNamespaceMapper == null) throw new ArgumentNullException("areaNamespaceMapper");

            this.kernel = kernel;
            this.areaNamespaceMapper = areaNamespaceMapper;
        }

        public void Register(AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory)
        {
            if (areaRegistrationContext == null) throw new ArgumentNullException("areaRegistrationContext");
            if (childKernelFactory == null) throw new ArgumentNullException("childKernelFactory");

            areaNamespaceMapper.Register(
                areaRegistrationContext.AreaName, 
                areaRegistrationContext.Namespaces.Select(ns => ns.TrimEnd('.', '*')).ToArray()
            );
            kernel.Bind<IChildKernel>()
                .ToMethod(c => childKernelFactory(c.Kernel))
                .InSingletonScope()
                .Named(areaRegistrationContext.AreaName);
        }

        public IKernel Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            var areaName = areaNamespaceMapper.Resolve(@namespace);
            if (areaName == null)
            {
                return kernel;
            }

            var areaKernel = kernel.TryGet<IChildKernel>(areaName);
            return areaKernel ?? kernel;
        }
    }
}
