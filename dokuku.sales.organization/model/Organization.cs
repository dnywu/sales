using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
namespace dokuku.sales.organization.model
{
    public class Organization
    {
        public string _id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int FiscalYearPeriod { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
    }
}