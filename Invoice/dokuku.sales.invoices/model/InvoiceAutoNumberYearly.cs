using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceAutoNumberYearly
    {
        public InvoiceAutoNumberYearly(string id, string companyId, int year)
        {
            _id = id;
            CompanyId = companyId;
            Year = year;
            Value = 0;
        }

        public string _id { get; private set; }
        public string CompanyId { get; private set; }
        public int Year { get; private set; }
        public int Value { get; private set; }

        public InvoiceAutoNumberYearly Next()
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
            return string.Format("{0}{1}{2}",
                prefix,
                Year.ToString(),
                Value.ToString().PadLeft(7, '0'));
        }
    }
}