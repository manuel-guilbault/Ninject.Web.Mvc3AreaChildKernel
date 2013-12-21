using System.Web.Mvc;

namespace MvcSample.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "MvcSample.Areas.Admin.Controllers" }
            );

            context.UseKernel(parent => new AdminKernel(parent));
        }
    }
}