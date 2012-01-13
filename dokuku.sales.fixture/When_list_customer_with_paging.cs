using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.customer;

namespace dokuku.sales.fixture
{
    [Subject("Get all customers")]
    public class When_list_customer_with_paging
    {
        private static ICustomerRepository csRepo;
        static Customer[] result;

        Establish context = () =>
        {
            csRepo = new CustomerRepository();
        };
        Because of = () =>
        {
            result = csRepo.LimitCustomers("randi@inforsys.co.id", 3, 2).ToArray();
        };
        It should_return_2_customers = () => {
            result.Length.ShouldEqual(2);
        };
    }
}