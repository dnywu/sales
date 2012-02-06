using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.group.model
{
    public class Group
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string Code { get; set; }
    }
   
}
