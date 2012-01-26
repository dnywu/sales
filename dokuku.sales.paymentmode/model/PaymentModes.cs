using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.paymentmode.model
{
    public class PaymentModes
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
