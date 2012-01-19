using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.customer.messages
{
    [Serializable]
    public class CustomerDeleted : IMessage
    {
        public Guid Id { get; set; }
    }
}
