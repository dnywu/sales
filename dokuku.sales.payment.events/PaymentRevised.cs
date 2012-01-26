using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class PaymentRevised : IMessage
    {
        public string PaymentJson { get; set; }
    }
}