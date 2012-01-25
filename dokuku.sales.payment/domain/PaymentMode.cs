using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class PaymentMode
    {
        private string p;

        public PaymentMode() { }
        public PaymentMode(string name)
        {
            Name = name;
        }
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}