using System;
using NServiceBus;

namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceForceCancelled : IMessage
    {
        public string Data { get; set; }
    }
}