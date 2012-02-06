using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.readmodel
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public string CityAddress { get; set; }
    }
}