using Ninject.Web.MvcAreaChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ninject.Web.MvcAreaChildKernel
{
    public static class AreaChildKernels
    {
        static IAreaChildKernelCollection collection = new AreaChildKernelCollection();

        public static IAreaChildKernelCollection Collection
        {
            get { return collection; }
        }

        public static void SetCollection(IAreaChildKernelCollection collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            AreaChildKernels.collection = collection;
        }
    }
}
