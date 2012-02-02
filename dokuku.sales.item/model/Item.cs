using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public class Item
    {
        public string OwnerId { get; set; }
        public Guid _id { get; set; }

        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Tax Tax { get; set; }
    }

    public class Tax
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}