using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceItemAdded
    {
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountInPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxAmount { get; set; }
        public Summary Summary { get; set; }
        public int ItemNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (!(obj is InvoiceItemAdded))
                return false;

            InvoiceItemAdded e = (InvoiceItemAdded)obj;
            return this.OwnerId == e.OwnerId &&
                this.UserName == e.UserName &&
                   this.InvoiceId == e.InvoiceId &&
                   this.ItemId == e.ItemId &&
                   this.Description == e.Description &&
                   this.Quantity == e.Quantity &&
                   this.Price == e.Price &&
                   this.DiscountInPercent == e.DiscountInPercent &&
                   this.DiscountAmount == e.DiscountAmount &&
                   this.Total == e.Total &&
                   this.TaxCode == e.TaxCode &&
                   this.TaxAmount == e.TaxAmount &&
                   this.Summary.Equals(e.Summary) &&
                   this.ItemNumber == e.ItemNumber;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
