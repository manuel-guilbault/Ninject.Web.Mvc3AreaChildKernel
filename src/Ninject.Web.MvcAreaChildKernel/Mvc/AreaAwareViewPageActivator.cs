using System;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel.Mvc
{
    /// <summary>
    /// View page activator that relies on a IKernelResolver to get the IKernel instance used
    /// to create the view page.
    /// </summary>
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

            var kernel = kernelResolver.Resolve(controllerContext.Controller.GetType().Namespace);
            return kernel.Get(type);
        }
    }
}
