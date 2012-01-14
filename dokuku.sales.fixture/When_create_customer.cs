using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.customer;
using dokuku.sales.customer.repository;
using dokuku.sales.customer.model;
namespace dokuku.sales.fixture
{
    [Subject("Creating customer")]
    public class When_create_customer
    {
        private static ICustomerRepository csRepo;
        private static ICustomerReportRepository csReportRepo;
        private static Customer cs;
        private static Guid id;
        private static string OwnerId;
        private static string custName;
        Establish context = () =>
            {
                csRepo = new CustomerRepository();
                csReportRepo = new CustomerReportRepository();
                id = Guid.NewGuid();
                OwnerId = "Oetawan@inforsys.co.id";
                custName = "Bulan Bintang";
            };

        Because of = () =>
            {
                csRepo.Save(new Customer() { 
                    _id =id,
                    OwnerId = OwnerId,
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
                cs = csReportRepo.GetByCustName(OwnerId, custName);
                cs.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                csRepo.Delete(id);
            };
    }
}