using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.events;
namespace dokuku.sales.invoices.command
{
    [Serializable]
    [MapsToAggregateRootConstructor("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain")]
    public class CreateInvoice : CommandBase
    {
        public Customer Customer { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Term Terms { get; set; }
        public DateTime DueDate { get; set; }
        public decimal ExchangeRate { get; set; }
        public string BaseCcy { get; set; }
        public string TransCcyCode { get; set; }
        public Guid InvoiceId { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        public CreateInvoice()
        {
        }
    }
}
