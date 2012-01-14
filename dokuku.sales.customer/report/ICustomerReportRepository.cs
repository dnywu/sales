using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;
namespace dokuku.sales.customer.repository
{
    public interface ICustomerReportRepository
    {
<<<<<<< HEAD:dokuku.sales.customer/report/ICustomerReportRepository.cs
=======
        void Save(Customer cs);
        Customer Get(Guid id);
        void Delete(Guid id);
        Customer GetByCustName(string ownerId, string custName);
>>>>>>> 01f8eb21df2e17059b48cc9253a08a9e000a436d:dokuku.sales.customer/ICustomerRepository.cs
        IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit);
        int CountCustomers(string ownerId);
    }

}
