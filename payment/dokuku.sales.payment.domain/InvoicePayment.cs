using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.payment.events;
using System.Diagnostics.Contracts;
using Ncqrs;
namespace dokuku.sales.payment.domain
{
    public class InvoicePayment : AggregateRootMappedByConvention
    {
        private decimal _amount;
        private decimal _balanceDue;
        private DateTime _invoiceDate;
        private string _ownerId;

        public InvoicePayment(string ownerId, Guid invoiceId, string invoiceNumber, DateTime invoiceDate, decimal amount) : base(invoiceId)
        {
            ApplyEvent(new InvoicePaymentCreated() {
                 InvoiceId = invoiceId,
                 InvoiceNumber = invoiceNumber,
                 InvoiceDate = invoiceDate,
                 Amount = amount,
                 BalanceDue = amount,
                 PaidOff = false,
                 OwnerId = ownerId
            });
        }
        public void PayInvoice(Guid paymentId, decimal amountPaid, decimal bankCharge, DateTime paymentDate, Guid paymentMode, string reference, string notes)
        {
            Contract.Requires(paymentDate.Date >= InvoiceDate, "Pembayaran hanya bisa dilakukan setelah atau pada hari yang bersamaan dengan tanggal invoice");
            Contract.Requires(amountPaid <= BalanceDue, "Jumlah yang dibayarkan melebihi sisa hutang yang harus dibayarkan");

            decimal balDue = _balanceDue - amountPaid;
            bool paidOff = balDue == 0;

            ApplyEvent(new InvoicePaid
            {
                InvoiceId = this.EventSourceId,
                PaymentId = paymentId,
                PaymentDate = paymentDate,
                AmountPaid = amountPaid,
                BankCharge = bankCharge,
                PaymentMode = paymentMode,
                Reference = reference,
                Notes = notes,
                BalanceDue = balDue,
                PaidOff = paidOff,
                OwnerId = _ownerId
            });
        }

        private void OnInvoicePaymentCreated(InvoicePaymentCreated @event)
        {
            _amount = @event.Amount;
            _balanceDue = @event.BalanceDue;
            _invoiceDate = @event.InvoiceDate.Date;
            _ownerId = @event.OwnerId;
        }
        private void OnInvoicePaid(InvoicePaid @event)
        {
            _balanceDue = @event.BalanceDue;
        }

        public InvoicePayment()
        {
        }
        public DateTime InvoiceDate { get { return _invoiceDate.Date; } }
        public decimal BalanceDue { get { return _balanceDue; } }
    }
}