using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.domainevents
{
    public class PaymentRevised : InvoicePaid
    {
        public Guid RevisedPaymentId { get; private set; }

        public PaymentRevised(Guid paymentRecordId,
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
            decimal balanceDue,
            Guid revisedPaymentId) : base(paymentRecordId,
                                          invoiceId, 
                                          invoiceNumber, 
                                          ownerId, 
                                          amountPaid, 
                                          bankCharge, 
                                          paymentDate,
                                          paymentMode,
                                          reference,
                                          notes,
                                          lunas,
                                          balanceDue)
        {
            this.RevisedPaymentId = revisedPaymentId;
        }
    }
}