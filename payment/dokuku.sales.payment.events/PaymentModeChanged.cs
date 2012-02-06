using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.common;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class PaymentModeChanged : PaymentEvent
    {
        public PaymentMode PaymentMode { get; set; }
    }
}