using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceRevised
    {
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Term Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string LateFee { get; set; }
        public string Note { get; set; }
        public string TermCondition { get; set; }
        public decimal ExchangeRate { get; set; }
        public string BaseCcy { get; set; }
        public string Currency { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public InvoiceItem[] Items { get; set; }
        public Guid _id { get; set; }
        public string OwnerId { get; set; }
        public string Status { get; set; }
        public string CancelNote { get; set; }
    }
}
