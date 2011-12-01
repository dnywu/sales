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
        public DateTime InvoiceDate { get; set; }
        public Invoice(Guid invoiceId, string invoiceNumber, decimal amount, DateTime invoiceDate)
        {
            this.InvoiceId = invoiceId;
            this.InvoiceNumber = invoiceNumber;
            this.Amount = amount;
            this.InvoiceDate = invoiceDate;
        }
    }
}