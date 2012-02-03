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
    public class AddInvoiceItem : CommandBase
    {
        [AggregateRootId]
        public Guid InvoiceId { get; set; }
        public string OwnerId {get;set;}
        public string UserName {get;set;}
        public Guid ItemId {get;set;}
        public string Description {get;set;}
        public int Quantity {get;set;}
        public decimal Price { get; set; }
        public decimal DiscountInPercent { get; set; }
        public Guid TaxId { get; set; }
    }
}
