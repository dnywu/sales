using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment
{
    public class InvoicePayment
    {
        public Invoice Invoice { get; set; }
        public Customer Customer { get; set; }
        public decimal BalanceDue { get; set; }

        public InvoicePayment(Invoice invoice, Customer customer)
        {
            this.Invoice = invoice;
            this.Customer = customer;
            this.BalanceDue = invoice.Amount;
        }

        public void Pay(PaymentRecord pr)
        {
            
        }
    }
}