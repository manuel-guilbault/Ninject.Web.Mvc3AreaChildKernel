using MvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Areas.Public.Models
{
    public class PublicService : IGenericService
    {
        private readonly DateTime createdAt = DateTime.Now;

        public string Message
        {
            get { return string.Format("Welcome in the public area (created at {0})!", createdAt); }
        }
    }
}