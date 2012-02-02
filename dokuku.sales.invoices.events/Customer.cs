using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.events
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string BillingAddress { get; private set; }
        public string CityAddress { get; private set; }
        public Customer(Guid id, string email, string name, string billingAddress, string cityAddress)
        {
            this.Id = id;
            this.Email = email;
            this.Name = name;
            this.BillingAddress = billingAddress;
            this.CityAddress = cityAddress;
        }
    }
}
