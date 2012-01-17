using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;
namespace dokuku.sales.customer.repository
{
    public interface ICustomerReportRepository
    {
        Customer GetByCustName(string ownerId, string custName);
        IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit);
        int CountCustomers(string ownerId);
    }
}