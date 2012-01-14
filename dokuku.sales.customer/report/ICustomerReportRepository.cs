<<<<<<< HEAD:dokuku.sales.customer/ICustomerRepository.cs
﻿using System;
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
        Customer GetByCustName(string ownerId, string custName);
        IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit);
        int CountCustomers(string ownerId);
    }
}
=======
﻿using System;
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
>>>>>>> origin/master:dokuku.sales.customer/report/ICustomerReportRepository.cs
