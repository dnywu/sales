using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.customer;
using dokuku.sales.customer.repository;
using dokuku.sales.customer.model;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Creating customer")]
    public class When_create_customer
    {
        private static ICustomerRepository csRepo;
        private static ICustomerReportRepository csReportRepo;
        private static Guid id;
        static MongoConfig mongo;
        static string ownerId = "oetawan@inforsys.co.id";

        Establish context = () =>
            {
                mongo = new MongoConfig();
                csRepo = new CustomerRepository(mongo);
                csReportRepo = new CustomerReportRepository(mongo);
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                csRepo.Save(new Customer() { 
                    _id =id,
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
            };

        It should_create_organization = () =>
            {
                Customer cs = csRepo.Get(id, ownerId);
                cs.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                csRepo.Delete(id);
            };
    }
}