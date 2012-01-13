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
        void Delete(Guid id);
<<<<<<< HEAD
        IEnumerable<Customer> AllCustomers();
        Customer GetByCustName(string ownerId, string custName);
=======
        IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit);
        int CountCustomers(string ownerId);
>>>>>>> origin/master
    }
}