using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.payment.messages
{
    [Serializable]
    public class RevisePayment : IMessage
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BankCharge { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid PaymentModeId { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
        public Guid AdjustedPaymentId { get; set; }
    }
}