using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.item.messages
{
    [Serializable]
    public class InvoiceFullPaid : IMessage
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Status { get; set; }
    }
}
