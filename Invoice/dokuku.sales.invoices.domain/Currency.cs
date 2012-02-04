using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public class Currency
    {
        public string Code { get; private set; }
        public int Rounding { get; private set; }
        public Currency(string code, int rounding)
        {
            this.Code = code;
            this.Rounding = rounding;
        }
    }
}
