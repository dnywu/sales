using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.payment.command;
using dokuku.sales.payment.domain;
using dokuku.sales.payment.query;
using Machine.Specifications;

namespace dokuku.sales.payment.fixture
{
    public class When_paymentMode_CRUD
    {
        static IPaymentModeCommand _paymentModeCommand;
        static IPaymentModeQuery _paymentModeQuery;
        static PaymentMode _paymentMode;
        static Guid _id = Guid.NewGuid();

        Establish context = () =>
        {
            _paymentModeCommand = new PaymentModeCommand(new MongoConfig());
            _paymentModeQuery = new PaymentModeQuery(new MongoConfig());
        };

        Because of = () => 
        {
            _paymentMode = new PaymentMode { _id = _id, Name = "Cash" };
        };

        It should_be_created_when_save = () => 
        {
            _paymentModeCommand.Save(_paymentMode);
        };

        It should_not_null_when_read = () => 
        {
            _paymentMode = _paymentModeQuery.Get(_id);
            _paymentMode.ShouldNotBeNull();
            _paymentMode.Name.ShouldEqual("Cash");
        };

        Cleanup cleanUp = () => 
        {
            _paymentModeCommand.Delete(_paymentMode._id);
        };
    }
}