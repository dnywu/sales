using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.taxes.model
{
    public class Taxes
    {
        public Guid _id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
