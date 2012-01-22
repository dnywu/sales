using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.payment.messages
{
    [Serializable]
    public class InvoicePartiallyPaid : IMessage
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string ownerid { get; set; }
    }
}