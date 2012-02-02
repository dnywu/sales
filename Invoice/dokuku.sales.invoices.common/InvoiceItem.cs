using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.common
{
    [Serializable]
    public class InvoiceItem
    {
            public Guid ItemId { get; set; }
            public string PartName { get; set; }
            public string Description { get; set; }
            public decimal Qty { get; set; }
            public decimal BasePrice { get; set; }
            public decimal Price { get; set; }
            public decimal Discount { get; set; }
            public Tax Tax { get; set; }
            public decimal BaseAmount { get; set; }
            public decimal Amount { get; set; }
    }
}
