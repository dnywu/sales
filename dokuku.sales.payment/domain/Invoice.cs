using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment
{
    public class Invoice
    {
        public string InvoiceNumber { get; private set; }
        public decimal Amount { get; private set; }

        public Invoice(string invoiceNumber, decimal amount)
        {
            this.InvoiceNumber = invoiceNumber;
            this.Amount = amount;
        }
    }
}