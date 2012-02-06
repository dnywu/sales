using System;
using NServiceBus;

namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceCancelled : IMessage
    {
        public string Data { get; set; }
    }
}