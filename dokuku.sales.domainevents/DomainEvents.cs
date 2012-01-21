using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.domainevents
{
    public static class DomainEvents
    {
        public static IContainer Container { get; set; }
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container != null)
                foreach (var handler in Container.ResolveAll<Handles<T>>())
                    handler.Handle(args);
        }
    }
}