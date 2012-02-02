using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain", "UpdateInvoiceExchangeRate")]
    public class ChangeInvoiceExchangeRate : CommandBase
    {
        [Parameter(1)]
        public Guid InvoiceId { get; set; }
        [Parameter(2)]
        public decimal ExchangeRate { get; set; }
        [Parameter(3)]
        public string UserName { get; set; }
    }
}