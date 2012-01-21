using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class PaymentRecord
    {
        public decimal amountPaid{get;private set;}
        public decimal bankCharge { get; private set; }
        public DateTime paymentDate { get; private set; }
        public PaymentMode paymentMode { get; private set; }
        public string reference { get; private set; }
        public string notes { get; private set; }
        
        private PaymentRecord(decimal amount)
        {
            amountPaid = amount;
        }
        public static PaymentRecord AmountPaid(decimal amount)
        {
            return new PaymentRecord(amount);
        }
        public PaymentRecord BankCharge(decimal charge)
        {
            bankCharge = charge;
            return this;
        }
        public PaymentRecord PaymentDate(DateTime date)
        {
            paymentDate = date;
            return this;
        }
        public PaymentRecord PaymentMode(PaymentMode mode)
        {
            paymentMode = mode;
            return this;
        }
        public PaymentRecord Reference(string reference)
        {
            this.reference = reference;
            return this;
        }
        public PaymentRecord Notes(string notes)
        {
            this.notes = notes;
            return this;
        }
    }
}