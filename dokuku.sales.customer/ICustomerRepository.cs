using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.customer
{
    public interface ICustomerRepository
    {
        void Save(Customer cs);
        Customer Get(Guid id);
        Customer Get(Guid id, string ownerId);
        void Delete(Guid id);
        Customer GetByCustName(string ownerId, string custName);
        IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit);
        int CountCustomers(string ownerId);
    }
}