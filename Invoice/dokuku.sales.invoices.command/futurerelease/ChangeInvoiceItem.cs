using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.common;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain","ChangeInvoiceItem")]
    public class ChangeInvoiceItem : CommandBase
    {
        [Parameter(1)]
        public Guid InvoiceId { get; set; }
        [Parameter(2)]
        public InvoiceItem Item { get; set; }
        [Parameter(3)]
        public string UserName { get; set; }
    }
}
