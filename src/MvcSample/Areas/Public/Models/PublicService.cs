using MvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Areas.Public.Models
{
    public class PublicService : IGenericService
    {
        public string Message
        {
            get { return "Welcome in the public area!"; }
        }
    }
}