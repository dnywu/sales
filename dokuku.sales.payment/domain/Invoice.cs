using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }

        public Invoice(Guid invoiceId, string invoiceNumber, decimal amount)
        {
            this.InvoiceId = invoiceId;
            this.InvoiceNumber = invoiceNumber;
            this.Amount = amount;
        }
    }
}