using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceAutoNumberDraft
    {
        public InvoiceAutoNumberDraft(string id, string companyId)
        {
            _id = id + "-" + companyId;
            CompanyId = companyId;
            Value = 0;
        }
        
        public string _id { get; private set; }
        public string CompanyId { get; private set; }
        public int Value { get; private set; }

        public InvoiceAutoNumberDraft Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string InvoiceNumberInStringFormat()
        {
            return string.Format("{0}{1}", "DRAFT-", Value.ToString());
        }
    }
}