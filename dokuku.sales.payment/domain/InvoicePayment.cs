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
        private Guid _id { get; set; }
        private string OwnerId { get; set; }
        private Invoice Invoice { get; set; }
        private decimal BalanceDue { get; set; }
        private bool SudahLunas { get { return BalanceDue == 0m; } }
        private IList<InvoicePaid> InvoicePaidEvents { get; set; }
        private IList<PaymentRevised> PaymentRevisedEvents { get; set; }

        public InvoicePayment(Guid id, string ownerId, Invoice invoice)
        {
            this._id = id;
            this.OwnerId = ownerId;
            this.Invoice = invoice;
            this.BalanceDue = invoice.Amount;
            this.InvoicePaidEvents = new List<InvoicePaid>();
            this.PaymentRevisedEvents = new List<PaymentRevised>();
        }
        
        public void Pay(Payment payment)
        {
            FailIfAmountPaidGreaterThanBalanceDue(payment);
            BalanceDue = BalanceDue - payment.amountPaid;
            InvoicePaid invoicePaid = new InvoicePaid(Guid.NewGuid(),
                this.Invoice.InvoiceId,
                this.Invoice.InvoiceNumber,
                this.OwnerId,
                payment.amountPaid,
                payment.bankCharge,
                payment.paymentDate,
                payment.paymentMode._id,
                payment.reference,
                payment.notes,
                SudahLunas,
                BalanceDue);
            InvoicePaidEvents.Add(invoicePaid);

            DomainEvents.Raise<InvoicePaid>(invoicePaid);
        }
        
        public void RevisePayment(Guid revisedPaymentRecordId, Payment payment)
        {
            Adjust(revisedPaymentRecordId);
            FailIfAmountPaidGreaterThanBalanceDue(payment);
            BalanceDue = BalanceDue - payment.amountPaid;

            PaymentRevised paymentRevised = new PaymentRevised(Guid.NewGuid(),
                this.Invoice.InvoiceId,
                this.Invoice.InvoiceNumber,
                this.OwnerId,
                payment.amountPaid,
                payment.bankCharge,
                payment.paymentDate,
                payment.paymentMode._id,
                payment.reference,
                payment.notes,
                SudahLunas,
                BalanceDue,
                revisedPaymentRecordId);

            DomainEvents.Raise<PaymentRevised>(paymentRevised);        }

        private void FailIfAmountPaidGreaterThanBalanceDue(Payment pr)
        {
            if (pr.amountPaid > this.BalanceDue)
                throw new PaymentExceedBalanceDueException();
        }
        
        private void Adjust(Guid paymentRecordId)
        {
            InvoicePaid adjusted = InvoicePaidEvents.Where(x => x.PaymentRecordId == paymentRecordId).FirstOrDefault();
            if (adjusted == null) return;

            BalanceDue = BalanceDue + adjusted.AmountPaid;
            
            PaymentRevised reversal = new PaymentRevised(Guid.NewGuid(),
                adjusted.InvoiceId,
                adjusted.InvoiceNumber,
                adjusted.OwnerId,
                0-adjusted.AmountPaid,
                0-adjusted.BankCharge,
                adjusted.PaymentDate,
                adjusted.PaymentMode,
                adjusted.Reference,
                "Reversal adjustment",
                SudahLunas,
                BalanceDue,
                adjusted.PaymentRecordId);
            PaymentRevisedEvents.Add(reversal);
        }
    }
}