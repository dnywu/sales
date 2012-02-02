using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.readmodel
{
    public class InvoiceIndex
    {
        public string OwnerId { get; private set; }
        public Guid _id { get; private set; }
        public string PONo { get; private set; }
        public Customer Customer { get; private set; }
        public string InvoiceNo { get; private set; }
        public String[] Keywords { get; private set; }
        public InvoiceIndex(Invoice Invoices) 
        {
            _id = Invoices.InvoiceId;
            this.OwnerId = Invoices.OwnerId;
            this.PONo = Invoices.PONo;
            this.Customer = Invoices.Customer;
            this.InvoiceNo = Invoices.InvoiceNo;
            buildKeywords(Invoices);
        }

        private void buildKeywords(Invoice Invoices)
        {
            Keywords = new string[] {
                Invoices.InvoiceId.ToString(),
                Invoices.OwnerId,
                Invoices.PONo,
                Invoices.Customer.Name,
                Invoices.InvoiceNo
            };
        }
    }
}
