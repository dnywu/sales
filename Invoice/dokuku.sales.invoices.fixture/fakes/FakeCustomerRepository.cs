using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.domain;
using dokuku.sales.invoices.repository;

namespace dokuku.sales.invoices.fixture
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(Guid customerId)
        {
            return new Customer(customerId, new Currency("IDR", 2), new Term("001",7));
        }
    }
}
