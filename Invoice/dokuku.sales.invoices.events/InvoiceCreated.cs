using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceCreated
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate {get;set;}
        public Guid CustomerId { get; set; }
        public string PONo { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        public string TermCode { get; set; }
        public DateTime DueDate { get; set; }
        public string TransactionCurrency { get; set; }
        public int DecimalPlace { get; set; }
        public string BaseCurrency { get; set; }
        public decimal Rate { get; set; }
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            if(object.ReferenceEquals(this,obj))
                return true;
            if (!(obj is InvoiceCreated))
                return false;

            InvoiceCreated e = (InvoiceCreated)obj;
            return this.BaseCurrency == e.BaseCurrency &&
                   this.CustomerId == e.CustomerId &&
                   this.DecimalPlace == e.DecimalPlace &&
                   this.DueDate == e.DueDate &&
                   this.InvoiceDate == e.InvoiceDate &&
                   this.InvoiceId == e.InvoiceId &&
                   this.InvoiceNo == e.InvoiceNo &&
                   this.OwnerId == e.OwnerId &&
                   this.PONo == e.PONo &&
                   this.Rate == e.Rate &&
                   this.Status == e.Status &&
                   this.TermCode == e.TermCode &&
                   this.TransactionCurrency == e.TransactionCurrency &&
                   this.UserName == e.UserName;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
