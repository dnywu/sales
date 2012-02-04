using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.events
{
    public class Tax
    {
        public string TaxCode { get; set; }
        public decimal TaxAmount { get; set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (!(obj is Tax))
                return false;

            Tax e = (Tax)obj;
            return this.TaxCode == e.TaxCode &&
                   this.TaxAmount == e.TaxAmount;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
