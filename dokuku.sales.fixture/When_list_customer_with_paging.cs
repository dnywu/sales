using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.customer.repository;
using dokuku.sales.customer.model;
using dokuku.sales.config;

namespace dokuku.sales.fixture
{
    [Subject("Query customer with limit")]
    public class When_list_customer_with_paging
    {
        private static ICustomerRepository csRepo;
        private static ICustomerReportRepository csReportRepo;
        static MongoConfig mongo;
        private static Guid id1;
        private static Guid id2;
        private static string ownerId = "oetawan@inforsys.co.id";
        static IEnumerable<Customer> result;

        Establish context = () =>
        {
            mongo = new MongoConfig();
            csRepo = new CustomerRepository(mongo);
            csReportRepo = new CustomerReportRepository(mongo);
            id1 = Guid.NewGuid();
            id2 = Guid.NewGuid();

            csRepo.Save(new Customer()
            {
                _id = id1,
                OwnerId = ownerId,
                BillingAddress = "Seipana",
                City = "Batam",
                Currency = "IDR",
                Email = "oetawan.ac@gmail.com",
                Salutation = "Mr. ",
                FirstName = "Oet",
                LastName = "Chandra",
                Fax = "472111",
                MobilePhone = "082173739678",
                Name = "Bulan bintang",
                Phone = "0778472111",
                PostalCode = "29432",
                State = "Kepri"
            });

            csRepo.Save(new Customer()
            {
                _id = id2,
                OwnerId = ownerId,
                BillingAddress = "Seipana",
                City = "Batam",
                Currency = "IDR",
                Email = "oetawan.ac@gmail.com",
                Salutation = "Mr. ",
                FirstName = "Oet",
                LastName = "Chandra",
                Fax = "472111",
                MobilePhone = "082173739678",
                Name = "Matahari",
                Phone = "0778472111",
                PostalCode = "29432",
                State = "Kepri"
            });
        };
        Because of = () =>
        {
            result = csReportRepo.LimitCustomers(ownerId, 1, 1).ToArray();
        };
        It should_return_1_customers = () => {
            result.ToArray().Length.ShouldEqual(1);
        };

        Cleanup cleanup = () =>
        {
            csRepo.Delete(id1);
            csRepo.Delete(id2);
        };
    }
}