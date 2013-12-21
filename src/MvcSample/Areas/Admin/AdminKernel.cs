using MvcSample.Areas.Admin.Models;
using MvcSample.Models;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Syntax;
using Ninject.Web.MvcAreaChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Areas.Admin
{
    public class AdminKernel : ChildKernel
    {
        public AdminKernel(IResolutionRoot parent)
            : base(parent, new NinjectSettings() { InjectAttribute = typeof(AreaInjectAttribute) })
        {
            Bind<IGenericService>().To<AdminService>();
        }
    }
}