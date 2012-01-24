using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;

namespace dokuku.sales.customer.Service
{
    public interface ICustomerService
    {
        Customer SaveCustomer(string customerJson,string ownerId);
        void UpdateCustomer(string customerJson);
        void DeleteCustomer(Guid id);
    }
}
