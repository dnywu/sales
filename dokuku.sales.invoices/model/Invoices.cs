using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.model
{
    public class Invoices
    {
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string LateFee { get; set; }
        public string Note { get; set; }
        public string TermCondition { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public InvoiceItems[] Items { get; set; }
        public Guid _id { get; set; }
        public string _rev { get; set; }
        public string OwnerId { get; set; }
    }

    public class InvoiceItems
    {
        public string PartName { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Amount { get; set; }     
        
    }
}
