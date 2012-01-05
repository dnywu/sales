using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
namespace dokuku.sales.organization
{
    public class Organization
    {
        const string ORGANIZATION_TYPE = "organization";

        public Guid _id { get; set; }
        public string _rev { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Cucrrency { get; set; }
        public int FiscalYearPeriod { get; set; }
        public string Type { get; private set; }

        public Organization(Guid id, string ownerId, string name, string ccy, int fiscalYearPeriod)
        {
            _id = id;
            OwnerId = ownerId;
            Name = name;
            Cucrrency = ccy;
            FiscalYearPeriod = fiscalYearPeriod;
            Type = ORGANIZATION_TYPE;
        }
    }
}