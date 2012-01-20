using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment
{
    public class PaymentMode
    {
        public PaymentMode(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}