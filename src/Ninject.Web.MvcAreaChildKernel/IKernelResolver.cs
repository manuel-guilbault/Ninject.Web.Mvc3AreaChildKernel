using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    public interface IKernelResolver
    {
        IKernel Resolve(Type type);
    }
}
