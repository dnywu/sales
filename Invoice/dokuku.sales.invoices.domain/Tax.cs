using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public class Tax
    {
        public string TaxCode { get; private set; }
        public decimal Rate { get; private set; }

        public Tax(string code, decimal rate)
        {
            TaxCode = code;
            Rate = rate;
        }
    }
}