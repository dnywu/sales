using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.command
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain","UpdateInvoiceDate")]
    public class ChangeInvoiceDate : CommandBase
    {
        [Parameter(1)]
        public DateTime InvoiceDate { get; set; }
        [Parameter(2)]
        public DateTime DueDate { get; set; }
        [Parameter(3)]
        public Guid InvoiceId { get; set; }
        [Parameter(4)]
        public string UserName { get; set; }
    }
}