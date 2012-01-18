using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceReports
    {
        public string OwnerId { get; set; }
        public string _id { get; set; }
        public string PONumber { get; set; }
        public string CustomerName { get; set; }

        public string[] Keywords { get; set; }
    }
}