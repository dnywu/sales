using System;
using NServiceBus;
namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceApproved : IMessage
    {
        public string InvoiceJson { get; set; }
    }
}