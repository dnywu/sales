using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.events
{
    public class Summary
    {
        public decimal SubTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public TaxSummary[] Taxes { get; set; }
        public decimal Charge { get; set; }
        public decimal NetTotal { get; set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (!(obj is Summary))
                return false;

            Summary e = (Summary)obj;
            return this.SubTotal == e.SubTotal &&
                   this.DiscountTotal == e.DiscountTotal &&
                   this.Charge == e.Charge &&
                   this.NetTotal == e.NetTotal &&
                   TaxesAreEquals(e);
        }
        public override int GetHashCode()
        {
            return 0;
        }

        private bool TaxesAreEquals(Summary that)
        {
            if (this.Taxes.Length != that.Taxes.Length) return false;
            if (this.Taxes.Length == 0) return true;
            bool areEqual = true;
            for (int i = 0; i < this.Taxes.Length; i++)
            {
                areEqual = this.Taxes[i].Equals(that.Taxes[i]);
                if (!areEqual)
                    break;
            }
            return areEqual;
        }
    }
}