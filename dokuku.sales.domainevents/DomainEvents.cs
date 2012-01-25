using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.domainevents
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;
        public static IContainer Container { get; set; }
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container != null)
                foreach (var handler in Container.ResolveAll<Handles<T>>())
                    handler.Handle(args);

            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
           if (actions == null)
              actions = new List<Delegate>();
     
           actions.Add(callback);
       }

       public static void ClearCallbacks ()
       {
           actions = null;
       }
    }
}