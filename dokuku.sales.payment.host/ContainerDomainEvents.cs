using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
using NServiceBus;
namespace dokuku.sales.payment.host
{
    public class ContainerDomainEvents : IContainer
    {
        public NServiceBus.ObjectBuilder.IBuilder ObjectBuilder { get; set; }
        public IEnumerable<T> ResolveAll<T>()
        {
            if (ObjectBuilder != null)
            {
                return ObjectBuilder.BuildAll<T>();
            }

            return new List<T>();
        }
    }
}