using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.payment.events
{
    [Serializable]
    public class PaymentModeCreated : IMessage
    {
        public string Data { get; set; }
    }
}
