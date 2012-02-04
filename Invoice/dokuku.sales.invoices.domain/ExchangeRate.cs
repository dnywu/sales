using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public class ExchangeRate
    {
        public decimal Rate { get; private set; }
        public ExchangeRate(Currency source, Currency target,decimal rate)
        {
            Rate = rate;
        }
    }
}
