using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.autonumbergenerator
{
    public class InvoiceAutoNumberMonthly
    {
        public InvoiceAutoNumberMonthly(string id, string companyId, int year, int month)
        {
            _id = id;
            CompanyId = companyId;
            Year = year;
            Month = month;
            Value = 0;
        }

        public string _id { get; private set; }
        public string CompanyId { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Value { get; private set; }
        
        public InvoiceAutoNumberMonthly Next()
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
            return string.Format("{0}{1}{2}{3}",
                prefix,
                Year.ToString(),
                Month.ToString().PadLeft(2,'0'),
                Value.ToString().PadLeft(5, '0'));
        }
    }
}