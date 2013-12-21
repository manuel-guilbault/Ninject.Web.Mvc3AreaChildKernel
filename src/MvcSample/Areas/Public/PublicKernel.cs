using MvcSample.Areas.Public.Models;
using MvcSample.Models;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Syntax;
using Ninject.Web.MvcAreaChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Areas.Public
{
    public class PublicKernel : ChildKernel
    {
        public PublicKernel(IResolutionRoot parent)
            : base(parent, new NinjectSettings() { InjectAttribute = typeof(AreaInjectAttribute) })
        {
            Bind<IGenericService>().To<PublicService>();
        }
    }
}