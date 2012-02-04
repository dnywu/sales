using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.paymentterms.model
{
    public class PaymentTerms
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string Code { get; set; }
        public string Term { get; set; }
    }
   
}
