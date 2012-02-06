using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using dokuku.sales.invoices.events;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.common;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain", "UpdateInvoiceTerm")]
    public class ChangeInvoiceTerm : CommandBase
    {
        [Parameter(1)]
        public Guid InvoiceId { get; set; }
        [Parameter(2)]
        public Term term { get; set; }
        [Parameter(3)]
        public DateTime DueDate { get; set; }
        [Parameter(4)]
        public string UserName { get; set; }
    }
}