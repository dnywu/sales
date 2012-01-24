using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.customer.model
{
    public class CustomerReports
    {
        public Guid _id { get; private set; }
        public string OwnerId { get; private set; }
        public String[] Keywords { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string BillingAddress { get; private set; }
        public CustomerReports(Customer customer)
        {
            _id = customer._id;
            this.OwnerId = customer.OwnerId;
            this.Name = customer.Name;
            this.Email = customer.Email;
            this.BillingAddress = customer.BillingAddress;
            buildKeyWords(customer);
        }

        private void buildKeyWords(Customer customer)
        {
            Keywords = new string[]{
                        customer._id.ToString(),
                        customer.Email,
                        customer.Name,
                        customer.OwnerId,
                        customer.BillingAddress
            };
        }
    }
}
