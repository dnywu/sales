using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain","ChangeQtyInvoiceItem")]
    public class ChangeQtyInvoiceItem : CommandBase
    {
        [Parameter(1)]
        public Guid InvoiceId { get; set; }
        [Parameter(2)]
        public Guid InvoiceItemId { get; set; }
        [Parameter(3)]
        public decimal InvoiceItemQty { get; set; }
        [Parameter(4)]
        public decimal InvoiceItemTotal { get; set; }
        [Parameter(5)]
        public string UserName { get; set; }
    }
}