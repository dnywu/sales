using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.command
{
    [MapsToAggregateRootMethod("dokuku.sales.invoices.Invoices,dokuku.sales.invoices","ApproveInvoice")]
    public class ApproveInvoice : CommandBase
    {
        [Parameter(1)]
        public Guid InvoiceId { get; set; }
        [Parameter(2)]
        public string InvoiceNo { get; set; }
        [Parameter(3)]
        public string OwnerId { get; set; }
        [Parameter(4)]
        public string UserName { get; set; }
    }
}