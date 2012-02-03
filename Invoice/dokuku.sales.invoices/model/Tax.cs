using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class Tax
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
        public decimal Amount { get; set; }
    }
}