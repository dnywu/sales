using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
namespace dokuku.sales.payment.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.payment.domain.InvoicePayment, dokuku.sales.payment.domain", "RevisePayment")]
    public class RevisePayment : Ncqrs.Commanding.CommandBase 
    {
        [AggregateRootId]
        public Guid InvoiceId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BankCharge { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid PaymentMode { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }
}