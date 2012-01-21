using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.domainevents
{
    public interface IContainer
    {
        IEnumerable<T> ResolveAll<T>();
    }
}