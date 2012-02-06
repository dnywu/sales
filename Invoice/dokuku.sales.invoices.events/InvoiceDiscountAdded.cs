using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.events
{
    public class InvoiceDiscountAdded
    {
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        public Guid InvoiceId { get; set; }
        public Summary Summary { get; set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (!(obj is InvoiceDiscountAdded))
                return false;

            InvoiceDiscountAdded e = (InvoiceDiscountAdded)obj;
            return this.OwnerId == e.OwnerId &&
                this.UserName == e.UserName &&
                this.InvoiceId == e.InvoiceId &&
                this.Summary.Equals(e.Summary);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
