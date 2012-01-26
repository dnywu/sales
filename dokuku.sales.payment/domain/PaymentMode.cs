using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class PaymentMode
    {
        public PaymentMode(Guid id, string name)
        {
            _id = id;
            Name = name;
        }
        public Guid _id { get; set; }
        public string Name { get; set; }
    }
}