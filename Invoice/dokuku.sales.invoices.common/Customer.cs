using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.common
{
    [Serializable]
    public class Customer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public string CityAddress { get; set; }
        public Term Term { get; set; }
        public Currency Currency { get; set; }
    }
}
