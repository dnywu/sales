using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.payment.commands;
namespace dokuku.sales.web.models
{
    public class InvoicePayment
    {
        public decimal AmountReceived { get; set; }
        public decimal BankChanges { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
        public string OwnerId { get; set; }
        public PaymentMode PaymentMethod { get; set; }
        public string Invoice { get; set; }
        public string Customer{ get; set; }
        public string CreditAvailable { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public Guid CustomerId{ get; set; }
        public Guid InvoiceId { get; set; }
        public string Tax { get; set; }
    }
}