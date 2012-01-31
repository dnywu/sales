using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.payment.readmodel;
using dokuku.sales.config;
namespace dokuku.sales.payment.integrationtest
{
    [Subject("Find all payments")]
    public class When_find_all_payments
    {
        static IEnumerable<Payment> result;
        static IPaymentRepository paymentRepository;
        
        Establish context = () => {
            paymentRepository = new PaymentRepository() { Mongo = new MongoConfig() };
        };

        Because of = () => {
            result = paymentRepository.FindAll("oetawan@inforsys.co.id");
        };

        It should_return_all_payments = () => {
            result.ShouldNotBeNull();
            result.Count().ShouldEqual(2);
        };
    }
}