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
        private const string defaultNamePrefix = "Area:";

        private readonly IKernel kernel;
        private readonly IAreaNamespaceMapper areaNamespaceMapper;
        private string namePrefix;

        public DefaultAreaChildKernelRegistry(IKernel kernel, IAreaNamespaceMapper areaNamespaceMapper)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (areaNamespaceMapper == null) throw new ArgumentNullException("areaNamespaceMapper");

            this.kernel = kernel;
            this.areaNamespaceMapper = areaNamespaceMapper;
        }

        public string NamePrefix
        {
            get
            {
                if (namePrefix == null)
                {
                    namePrefix = defaultNamePrefix;
                }
                return defaultNamePrefix;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();

                namePrefix = value;
            }
        }

        protected virtual string GetChildKernelName(string areaName)
        {
            return NamePrefix + areaName;
        }

        public void Register(AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory)
        {
            if (areaRegistrationContext == null) throw new ArgumentNullException("areaRegistrationContext");
            if (childKernelFactory == null) throw new ArgumentNullException("childKernelFactory");

            areaNamespaceMapper.Register(
                areaRegistrationContext.AreaName, 
                areaRegistrationContext.Namespaces.ToArray()
            );

            kernel.Bind<IChildKernel>()
                .ToConstant(childKernelFactory(kernel))
                .Named(GetChildKernelName(areaRegistrationContext.AreaName));
        }

        public IKernel Resolve(string @namespace)
        {
            if (@namespace == null) throw new ArgumentNullException("namespace");

            var areaName = areaNamespaceMapper.Resolve(@namespace);
            if (areaName == null)
            {
                return kernel;
            }

            var areaKernel = kernel.TryGet<IChildKernel>(GetChildKernelName(areaName));
            return areaKernel ?? kernel;
        }
    }
}
