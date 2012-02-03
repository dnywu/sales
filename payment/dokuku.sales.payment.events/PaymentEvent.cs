using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public abstract class PaymentEvent 
    {
        public string OwnerId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid PaymentId { get; set; }
        public string Username { get; set; }
    }
}