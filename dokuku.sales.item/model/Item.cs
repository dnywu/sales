using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public class Item
    {
        public Item()
        {
            Type = "item";
        }

        public string Type { get; private set; }
        public string OwnerId { get; set; }
        public Guid _id { get; set; }
        public string _rev { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Tax Tax { get; set; }
    }

    public class Tax
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}