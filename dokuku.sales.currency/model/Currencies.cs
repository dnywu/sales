using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.currency.model
{
    public class Currencies
    {
        public Guid _id {get;set;}
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Rounding { get; set; }
        public string OwnerId { get; set; }
    }
}
