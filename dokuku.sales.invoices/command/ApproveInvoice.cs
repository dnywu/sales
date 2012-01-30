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
        public Guid Id { get; private set; }
        public string InvoiceNo { get; private set; }
        public string OwnerId { get; private set; }
        public ApproveInvoice(Guid id, string invoiceNo, string ownerId)
        {
            this.Id = id;
            this.InvoiceNo = invoiceNo;
            this.OwnerId = ownerId;
        }
    }
}
