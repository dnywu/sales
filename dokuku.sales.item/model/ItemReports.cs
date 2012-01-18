using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.item.model
{
    public class ItemReports
    {
        public string OwnerId { get; private set; }
        public Guid _id { get; private set; }
        public string Code { get; private set; }
        public string Barcode { get; private set; }
        public string Name { get; private set; }
        public String[] Keywords { get; private set; }
        public ItemReports(Item item)
        {
            OwnerId = item.OwnerId;
            _id = item._id;
            Code = item.Code;
            Barcode = item.Barcode;
            Name = item.Name;
            buildKeywords(item);
        }

        private void buildKeywords(Item item)
        {
            Keywords = new string[] {
                        item.OwnerId,
                        item._id.ToString(),
                        item.Code,
                        item.Barcode,
                        item.Name
            };
        }
    }
}
