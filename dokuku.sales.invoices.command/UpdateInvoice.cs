using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.common;
//using dokuku.sales.invoices.events;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoice, dokuku.sales.invoices.domain", "UpdateInvoice")]
    public class UpdateInvoice: CommandBase
    {
        [AggregateRootId]
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Term Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string Note { get; set; }
        public decimal ExchangeRate { get; set; }
        public string BaseCcy { get; set; }
        public string Currency { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public InvoiceItem[] Items { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        public string TermCondition { get; set; }
        public UpdateInvoice()
        {
        }
    }
}
