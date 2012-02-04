using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.commands
{
    [MapsToAggregateRootMethod("dokuku.sales.invoices.Invoices,dokuku.sales.invoices","ApproveInvoice")]
    public class ApproveInvoice : CommandBase
    {
        [AggregateRootId]
        public Guid InvoiceId { get; set; }
        public string OwnerId { get; set; }
    }
}