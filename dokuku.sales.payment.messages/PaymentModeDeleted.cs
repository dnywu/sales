using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.payment.messages
{
    [Serializable]
    public class PaymentModeDeleted : IMessage
    {
        public Guid Id { get; set; }
    }
}
