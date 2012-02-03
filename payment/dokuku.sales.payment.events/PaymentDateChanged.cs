using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.common;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class PaymentDateChanged : PaymentEvent
    {
        public DateTime PaymentDate { get; set; }
    }
}