using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceCreated : IMessage
    {
        public string InvoiceJson { get; set; }
    }
}