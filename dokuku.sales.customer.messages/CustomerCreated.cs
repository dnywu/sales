using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.customer.messages
{
    [Serializable]
    public class CustomerCreated : IMessage
    {
        public string Data { get; set; }
    }
}
