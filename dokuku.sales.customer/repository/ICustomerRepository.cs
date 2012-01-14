using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;
namespace dokuku.sales.customer.repository
{
    public interface ICustomerRepository
    {
        void Save(Customer cs);
        void Delete(Guid id);
        Customer Get(Guid id, string ownerId);
    }
}