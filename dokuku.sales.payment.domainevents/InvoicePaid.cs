using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.domainevents
{
    public class InvoicePaid : IDomainEvent
    {
        public Guid PaymentRecordId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string InvoiceNumber { get; private set; }
        public string OwnerId { get; private set; }

        public decimal AmountPaid { get; private set; }
        public decimal BankCharge { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public Guid PaymentMode { get; private set; }
        public string Reference { get; private set; }
        public string Notes { get; private set; }
        public bool Lunas { get; private set; }
        public decimal BalanceDue { get; private set; }

        public InvoicePaid(Guid paymentRecordId,
            Guid invoiceId,
            string invoiceNumber,
            string ownerId,
            decimal amountPaid,
            decimal bankCharge,
            DateTime paymentDate,
            Guid paymentMode,
            string reference,
            string notes,
            bool lunas,
            decimal balanceDue)
        {
            this.InvoiceId = invoiceId;
            this.InvoiceNumber = invoiceNumber;
            this.OwnerId = ownerId;
            this.AmountPaid = amountPaid;
            this.BankCharge = bankCharge;
            this.PaymentDate = paymentDate;
            this.PaymentMode = paymentMode;
            this.Reference = reference;
            this.Notes = notes;
            this.Lunas = lunas;
            this.BalanceDue = balanceDue;
        }
    }
}