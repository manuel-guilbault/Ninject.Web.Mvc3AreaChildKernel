using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.MvcAreaChildKernel
{
    public interface IAreaNamespaceMapCache
    {
        string Resolve(string @namespace);
        void Map(string @namespace, string areaName);

        bool IsIgnored(string @namespace);
        void Ignore(string @namespace);

        void Clear();
    }
}
