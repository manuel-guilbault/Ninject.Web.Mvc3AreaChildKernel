using MvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Areas.Admin.Models
{
    public class AdminService : IGenericService
    {
        public string Message
        {
            get { return "Welcome in the admin area!"; }
        }
    }
}