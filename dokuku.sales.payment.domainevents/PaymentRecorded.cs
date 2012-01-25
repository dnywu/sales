using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.domainevents
{
    public class PaymentRecorded : IDomainEvent
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BankCharge { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string PRReference { get; set; }
        public string PRNotes { get; set; }
        public Guid PaymentRecordId { get; set; }
        public string OwnerId { get; set; }
    }
}