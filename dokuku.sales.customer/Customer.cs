﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.customer
{
    public class Customer
    {
        public Customer()
        {
            Type = "customer";
        }

        public string Name { get; set; }
        public string Currency { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Fax { get; set; }
        public string Type { get; set; }

        public Guid _id { get; set; }
        public string _rev { get; set; }
        public string OwnerId { get; set; }
    }
}