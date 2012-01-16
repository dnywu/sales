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
    [Subject("Get all customers")]
    public class When_list_customer_with_paging
    {
        private static ICustomerRepository csRepo;
        private static ICustomerReportRepository csReportRepo;
        static Customer[] result;
        static MongoConfig mongo;
        Establish context = () =>
        {
            csRepo = new CustomerRepository(mongo);
            csReportRepo = new CustomerReportRepository(mongo);
        };
        Because of = () =>
        {
            result = csReportRepo.LimitCustomers("randi@inforsys.co.id", 3, 2).ToArray();
        };
        It should_return_2_customers = () => {
            result.Length.ShouldEqual(2);
        };
    }
}