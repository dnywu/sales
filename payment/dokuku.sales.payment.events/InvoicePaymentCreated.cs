using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.events
{
    [Serializable]
    public class InvoicePaymentCreated
    {
        public string OwnerId { get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceDue { get; set; }
        public bool PaidOff { get; set; }
        public string Username { get; set; }
    }
}