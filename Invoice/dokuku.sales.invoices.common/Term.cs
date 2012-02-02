using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.common
{
    [Serializable]
    public class Term
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public Term(string name, decimal value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
