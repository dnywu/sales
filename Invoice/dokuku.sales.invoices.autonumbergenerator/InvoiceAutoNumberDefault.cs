using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.autonumbergenerator
{
    public class InvoiceAutoNumberDefault
    {
        public InvoiceAutoNumberDefault(string id, string companyId)
        {
            _id = id;
            CompanyId = companyId;
            Value = 0;
        }
        
        public string _id { get; private set; }
        public string CompanyId { get; private set; }
        public int Value { get; private set; }
        
        public InvoiceAutoNumberDefault Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string InvoiceNumberInStringFormat(string prefix)
        {
            return string.Format("{0}{1}", prefix, Value.ToString());
        }
    }
}