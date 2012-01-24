using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
using dokuku.sales.payment.domainevents;
namespace dokuku.sales.payment.domain
{
    public class InvoicePayment
    {
        public Guid _id { get; private set; }
        public string OwnerId { get; private set; }
        public Invoice Invoice { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal BalanceDue { get; private set; }
        public IList<PaymentRecord> PaymentRecords { get; private set; }

        public InvoicePayment(Guid id, string ownerId, Invoice invoice, Guid customerId)
        {
            this._id = id;
            this.OwnerId = ownerId;
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
                DomainEvents.Raise<InvoiceSudahLunas>(new InvoiceSudahLunas { InvoiceNumber = this.Invoice.InvoiceNumber, InvoiceId = this.Invoice.InvoiceId, ownerid = this.OwnerId });
            if (HasOutstanding())
                DomainEvents.Raise<InvoiceDibayarSebagian>(new InvoiceDibayarSebagian { InvoiceNumber = this.Invoice.InvoiceNumber, InvoiceId = this.Invoice.InvoiceId, ownerid = this.OwnerId });
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