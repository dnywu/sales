using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceUpdate:IMessage
    {
        public string Data { get; set; }
    }
}
