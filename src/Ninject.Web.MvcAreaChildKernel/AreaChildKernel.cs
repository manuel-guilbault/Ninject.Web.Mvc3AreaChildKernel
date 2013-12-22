using Ninject.Syntax;
using Ninject.Extensions.ChildKernel;
using System;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class AreaChildKernel
    {
        private readonly string areaName;
        private readonly Func<IKernel, IChildKernel> factory;

        private bool isBound = false;

        public AreaChildKernel(string areaName, Func<IKernel, IChildKernel> factory)
        {
            if (areaName == null) throw new ArgumentNullException("areaName");
            if (factory == null) throw new ArgumentNullException("factory");

            this.areaName = areaName;
            this.factory = factory;
        }

        public string AreaName
        {
            get { return areaName; }
        }

        public IChildKernel GetChildKernel(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");

            if (!isBound)
            {
                kernel.Bind<IChildKernel>().ToMethod(c => factory(c.Kernel)).InSingletonScope().Named(areaName);
                isBound = true;
            }

            return kernel.Get<IChildKernel>(areaName);
        }
    }
}
