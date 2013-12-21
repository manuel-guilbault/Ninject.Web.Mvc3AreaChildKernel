using System.Web.Mvc;

namespace MvcSample.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Public_default",
                "Public/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "MvcSample.Areas.Public.Controllers" }
            );

            context.UseKernel(parent => new PublicKernel(parent));
        }
    }
}