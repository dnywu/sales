using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.payment;
using dokuku.sales.payment.common;
namespace dokuku.sales.web.models
{
    [Serializable]
    public class PayInvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BankCharge { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }
}