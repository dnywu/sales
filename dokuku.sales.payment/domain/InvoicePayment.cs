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
            PaymentRecords.Add(pr);
            CalculateBalanceDue();

            DomainEvents.Raise<InvoicePaid>(new InvoicePaid
            {
                AmountPaid = pr.amountPaid,
                BankCharge = pr.bankCharge,
                InvoiceId = this.Invoice.InvoiceId,
                InvoiceNumber = this.Invoice.InvoiceNumber,
                PaymentDate = pr.paymentDate,
                PaymentMode = pr.paymentMode.Name,
                PaymentRecordId = pr.Id,
                PRNotes = pr.notes,
                PRReference = pr.reference,
                OwnerId = this.OwnerId,
                Lunas = !HasOutstanding()
            });
        }
        
        public void RevisePayment(Guid revisedPaymentRecordId, PaymentRecord newPaymentRecord)
        {
            Adjust(revisedPaymentRecordId);
            FailIfAmountPaidGreaterThanBalanceDue(newPaymentRecord);
            PaymentRecords.Add(newPaymentRecord);
            CalculateBalanceDue();

            DomainEvents.Raise<PaymentRevised>(new PaymentRevised
            {
                AmountPaid = newPaymentRecord.amountPaid,
                BankCharge = newPaymentRecord.bankCharge,
                InvoiceId = this.Invoice.InvoiceId,
                InvoiceNumber = this.Invoice.InvoiceNumber,
                PaymentDate = newPaymentRecord.paymentDate,
                PaymentMode = newPaymentRecord.paymentMode.Name,
                PaymentRecordId = revisedPaymentRecordId,
                PRNotes = newPaymentRecord.notes,
                PRReference = newPaymentRecord.reference,
                OwnerId = this.OwnerId,
                Lunas = !HasOutstanding()
            });
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

        private void Adjust(Guid paymentRecordId)
        {
            PaymentRecord adjusted = PaymentRecords.Where(x => x.Id == paymentRecordId).FirstOrDefault();
            if (adjusted == null) return;
            
            PaymentRecord reversal = adjusted.Reverse();
            PaymentRecords.Add(reversal);
            CalculateBalanceDue();
        }

        private void CalculateBalanceDue()
        {
            decimal amountPaid = 0;
            foreach (PaymentRecord p in PaymentRecords)
                amountPaid += p.amountPaid;
            BalanceDue = Invoice.Amount - amountPaid;
        }
    }
}