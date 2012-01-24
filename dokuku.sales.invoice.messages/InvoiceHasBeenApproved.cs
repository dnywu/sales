using System;
using NServiceBus;

namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceHasBeenApproved : IMessage
    {
        public Guid InvoiceId { get; set; }
        public string OwnerId { get; set; }
    }
}