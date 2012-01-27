using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceItem
    {
        public Guid ItemId { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal BaseRate { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public Tax Tax { get; set; }
        public decimal Amount { get; set; }
    }
}