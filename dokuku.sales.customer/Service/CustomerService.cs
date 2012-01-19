using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;
using dokuku.sales.customer.repository;
using NServiceBus;

namespace dokuku.sales.customer.Service
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository repo;
        IBus bus;
        public CustomerService(ICustomerRepository repo, IBus bus)
        {
            this.repo = repo;
            this.bus = bus;
        }
        public void Save(Customer cs)
        {
            repo.Save(cs);
            CreateIndex(cs);
            bus.Publish<>();
        }

        private CustomerReports CreateIndex(Customer customer)
        {
            return new CustomerReports(customer);
        }

        public void UpdateCustomer(Customer cust)
        {
            repo.UpdateCustomer(cust);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(Guid id, string ownerId)
        {
            throw new NotImplementedException();
        }
    }
}
