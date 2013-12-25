using System;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    public class AreaAwareViewPageActivator : IViewPageActivator
    {
        private readonly IKernelResolver kernelResolver;

        public AreaAwareViewPageActivator(IKernelResolver kernelResolver)
        {
            if (kernelResolver == null) throw new ArgumentNullException("kernelResolver");

            this.kernelResolver = kernelResolver;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");
            if (type == null) throw new ArgumentNullException("type");

            var kernel = kernelResolver.Resolve(controllerContext.Controller.GetType());
            return kernel.Get(type);
        }
    }
}
