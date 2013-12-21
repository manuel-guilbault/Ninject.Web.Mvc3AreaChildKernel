using MvcSample.Filters;
using MvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSample.Areas.Admin.Controllers
{
    [MessageHeaderFilter]
    public class HomeController : Controller
    {
        readonly IGenericService service;

        public HomeController(IGenericService service)
        {
            if (service == null) throw new ArgumentNullException("service");

            this.service = service;
        }

        public ActionResult Index()
        {
            ViewBag.Message = service.Message;

            return View();
        }
	}
}