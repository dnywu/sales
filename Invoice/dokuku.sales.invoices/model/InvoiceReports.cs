using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceReports
    {
        public string OwnerId { get; private set; }
        public Guid _id { get; private set; }
        public string PONo { get; private set; }
        public string Customer { get; private set; }
        public string InvoiceNo { get; private set; }
        public String[] Keywords { get; private set; }
        public InvoiceReports(Invoices Invoices) 
        {
            _id = Invoices._id;
            this.OwnerId = Invoices.OwnerId;
            this.PONo = Invoices.PONo;
            this.Customer = Invoices.Customer;
            this.InvoiceNo = Invoices.InvoiceNo;
            buildKeywords(Invoices);
        }

        private void buildKeywords(Invoices Invoices)
        {
            Keywords = new string[] {
                Invoices._id.ToString(),
                Invoices.OwnerId,
                Invoices.PONo,
                Invoices.Customer,
                Invoices.InvoiceNo
            };
        }
    }
}