using System;

namespace Ninject.Web.MvcAreaChildKernel
{
    public static class AreaChildKernels
    {
        private static IAreaChildKernelCollection collection = new AreaChildKernelCollection();

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
