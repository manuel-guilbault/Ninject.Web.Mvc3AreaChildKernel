using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareControllerActivator : IControllerActivator
    {
        readonly IKernel kernel;

        public AreaAwareControllerActivator(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");

            this.kernel = kernel;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)kernel.ResolveChildKernelFor(controllerType).Get(controllerType);
        }
    }
}
