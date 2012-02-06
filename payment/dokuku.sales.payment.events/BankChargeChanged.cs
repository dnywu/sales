using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class BankChargeChanged : PaymentEvent
    {
        public decimal BankCharge { get; set; }
    }
}