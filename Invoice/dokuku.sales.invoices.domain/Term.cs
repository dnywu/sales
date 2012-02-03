using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.domain
{
    public class Term
    {
        public string Code { get; private set; }
        public int Value { get; private set; }
        public Term(string code, int value)
        {
            this.Code = code;
            this.Value = value;
        }
    }
}
