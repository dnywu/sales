using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
namespace dokuku.sales.payment.commands
{
    abstract public class PaymentCommand : CommandBase
    {
        [AggregateRootId]
        public Guid InvoiceId { get; set; }
        public Guid PaymentId { get; set; }
        public string Username { get; set; }
    }
}