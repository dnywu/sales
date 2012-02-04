using System;
using NServiceBus;

namespace dokuku.sales.invoice.messages
{
    [Serializable]
    public class InvoiceUpdate : IMessage
    {
        public string Data { get; set; }
    }
}
