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
using dokuku.sales.customer.Service;
using MongoDB.Bson;
namespace dokuku.sales.fixture
{
    [Subject("Count customers")]
    public class When_count_customer
    {
        private static ICustomerService csService;
        private static ICustomerReportRepository csReportRepo;
        private static Guid id1;
        private static Guid id2;
        static MongoConfig mongo;
        static string ownerId = "oetawan@inforsys.co.id";
        static int result = 0;

        Establish context = () =>
            {
                mongo = new MongoConfig();
                csService = new CustomerService(mongo,null);
                csReportRepo = new CustomerReportRepository(mongo);
                id1 = Guid.NewGuid();
                id2 = Guid.NewGuid();

                csService.SaveCustomer(BsonDocument.Create(new Customer()
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
                }).ToJson(), ownerId);

                csService.SaveCustomer(BsonDocument.Create(new Customer()
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
                }).ToJson(),ownerId);
            };

        Because of = () =>
            {
                result = csReportRepo.CountCustomers(ownerId);
            };

        It should_return_number_of_items = () =>
            {
                result.ShouldEqual(2);
            };

        Cleanup cleanup = () =>
            {
                csService.DeleteCustomer(id1);
                csService.DeleteCustomer(id2);
            };
    }
}