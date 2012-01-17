using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
namespace dokuku.sales.organization.model
{
    public class Organization
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Cucrrency { get; set; }
        public int FiscalYearPeriod { get; set; }

        public Organization(string id, string name, string ccy, int fiscalYearPeriod)
        {
            _id = id;
            Name = name;
            Cucrrency = ccy;
            FiscalYearPeriod = fiscalYearPeriod;
        }
    }
}