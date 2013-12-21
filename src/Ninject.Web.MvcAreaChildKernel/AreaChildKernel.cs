using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    public class AreaChildKernel
    {
        readonly string name;
        readonly Func<IKernel, IChildKernel> factory;

        bool isBound;

        public AreaChildKernel(string name, Func<IKernel, IChildKernel> factory)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (factory == null) throw new ArgumentNullException("factory");

            this.name = name;
            this.factory = factory;
        }

        public string Name
        {
            get { return name; }
        }

        public IChildKernel GetChildKernel(IKernel kernel)
        {
            if (!isBound)
            {
                kernel.Bind<IChildKernel>().ToMethod(c => factory(c.Kernel)).Named(name);
                isBound = true;
            }

            return kernel.Get<IChildKernel>(name);
        }
    }
}
