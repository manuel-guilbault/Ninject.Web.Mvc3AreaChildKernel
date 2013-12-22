using MvcSample.Models;
using Ninject;
using Ninject.Web.MvcAreaChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSample.Filters
{
    public class MessageHeaderFilterAttribute : FilterAttribute, IResultFilter
    {
        [Inject]
        public IGenericService Service { get; set; }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("X-Message", Service.Message);
        }
    }
}