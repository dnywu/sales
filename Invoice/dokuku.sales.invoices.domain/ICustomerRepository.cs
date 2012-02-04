using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.domain;
namespace dokuku.sales.invoices.domain
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(Guid customerId);
    }
}