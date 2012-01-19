using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;

namespace dokuku.sales.customer.Service
{
    public interface ICustomerService
    {
        void Save(Customer cs);
        void UpdateCustomer(Customer cust);
        void Delete(Guid id);
        Customer Get(Guid id, string ownerId);
    }
}
