using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public interface IAreaChildKernelRegistrar
    {
        void Register(AreaRegistrationContext areaRegistrationContext, Func<IKernel, IChildKernel> childKernelFactory);
    }
}
