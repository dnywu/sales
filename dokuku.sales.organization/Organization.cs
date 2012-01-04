using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.organization
{
    public class Organization
    {
        const string ORGANIZATION_TYPE = "organization";

        public string _id { get; set; }
        public string _rev { get; set; }
        public string Name { get; set; }
        public string Cucrrency { get; set; }
        public int FiscalYearPeriod { get; set; }
        public string Type { get; private set; }

        public Organization(string id, string name, string ccy, int fiscalYearPeriod)
        {
            _id = id;
            Name = name;
            Cucrrency = ccy;
            FiscalYearPeriod = fiscalYearPeriod;
            Type = ORGANIZATION_TYPE;
        }
    }
}