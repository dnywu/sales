using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.repository.impl
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(Guid customerId)
        {
            return new Customer(Guid.NewGuid(), new Currency("IDR",2), new Term("001",7));
        }
    }
}
