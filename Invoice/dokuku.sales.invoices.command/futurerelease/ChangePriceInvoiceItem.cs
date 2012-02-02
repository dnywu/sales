using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain","ChangeTotalInvoiceItem")]
    public class ChangePriceInvoiceItem : CommandBase
    {
        public Guid InvoiceId { get; set; }
        public Guid InvoiceItemid { get; set; }
        public decimal InvoiceItemPrice { get; set; }
        public decimal InvoiceItemTotal { get; set; }
        public string UserName { get; set; }
    }
}