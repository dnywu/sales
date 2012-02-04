using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.events
{
    public class TaxSummary
    {
        public string TaxCode { get; set; }
        public decimal TaxAmount { get; set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (!(obj is TaxSummary))
                return false;

            TaxSummary e = (TaxSummary)obj;
            return this.TaxCode == e.TaxCode &&
                   this.TaxAmount == e.TaxAmount;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
