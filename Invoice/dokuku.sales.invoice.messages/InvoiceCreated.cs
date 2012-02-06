using System;
using NServiceBus;
namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceCreated : IMessage
    {
        public string InvoiceJson { get; set; }
    }
}