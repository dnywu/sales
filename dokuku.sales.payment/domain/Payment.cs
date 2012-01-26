using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class Payment
    {
        public decimal amountPaid{get; private set;}
        public decimal bankCharge { get; private set; }
        public DateTime paymentDate { get; private set; }
        public PaymentMode paymentMode { get; private set; }
        public string reference { get; private set; }
        public string notes { get; private set; }
        public Guid Id { get; private set; }
        public Guid adjustedPaymentRecordId { get; private set; }

        private Payment(decimal amount)
        {
            amountPaid = amount;
            Id = Guid.NewGuid();
        }
        public static Payment AmountPaid(decimal amount)
        {
            return new Payment(amount);
        }
        public Payment BankCharge(decimal charge)
        {
            bankCharge = charge;
            return this;
        }
        public Payment PaymentDate(DateTime date)
        {
            paymentDate = date;
            return this;
        }
        public Payment PaymentMode(PaymentMode mode)
        {
            paymentMode = mode;
            return this;
        }
        public Payment Reference(string reference)
        {
            this.reference = reference;
            return this;
        }
        public Payment Notes(string notes)
        {
            this.notes = notes;
            return this;
        }
        public Payment AdjustedPaymentRecordId(Guid id)
        {
            this.adjustedPaymentRecordId = id;
            return this;
        }
    }
}