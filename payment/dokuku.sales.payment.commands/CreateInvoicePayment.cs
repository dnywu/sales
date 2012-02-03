using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.payment.commands
{
    [Serializable]
    [MapsToAggregateRootConstructor("dokuku.sales.payment.domain.InvoicePayment, dokuku.sales.payment.domain")]
    public class CreateInvoicePayment : CommandBase
    {
        public string OwnerId { get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string Username { get; set; }
    }
}