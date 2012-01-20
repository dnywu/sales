using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.domain
{
    public class InvoicePayment
    {
        public Invoice Invoice { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal BalanceDue { get; private set; }
        public IList<PaymentRecord> PaymentRecords { get; private set; }

        public InvoicePayment(Invoice invoice, Guid customerId)
        {
            this.Invoice = invoice;
            this.CustomerId = customerId;
            this.BalanceDue = invoice.Amount;
            this.PaymentRecords = new List<PaymentRecord>();
        }

        public void Pay(PaymentRecord pr)
        {
            FailIfAmountPaidGreaterThanBalanceDue(pr);
            BalanceDue = BalanceDue - pr.amountPaid;
            PaymentRecords.Add(pr);
            if (!HasOutstanding())
                DomainEvents.Raise<InvoiceSudahLunas>(new InvoiceSudahLunas { InvoiceNumber = this.Invoice.InvoiceNumber, InvoiceId = this.Invoice.InvoiceId });
        }

        private void FailIfAmountPaidGreaterThanBalanceDue(PaymentRecord pr)
        {
            if (pr.amountPaid > this.BalanceDue)
                throw new PaymentExceedBalanceDueException();
        }

        public bool HasOutstanding()
        {
            return BalanceDue > 0;
        }
    }
}