using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class AmountPaidChanged : PaymentEvent
    {
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public bool PaidOff { get; set; }
    }
}