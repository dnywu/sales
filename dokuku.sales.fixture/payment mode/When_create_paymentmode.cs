using System;
using Machine.Specifications;
using dokuku.sales.config;
using dokuku.sales.paymentmode.service;
using dokuku.sales.paymentmode.query;
using dokuku.sales.paymentmode.model;
using MongoDB.Bson;
using Newtonsoft.Json;
namespace dokuku.sales.fixture.payment_mode
{
    [Subject("Create PaymentMode")]
    public class When_create_paymentmode
    {
        static MongoConfig config;
        static IPaymentModeService payModeService;
        static IPaymentModeQuery payModeQuery;
        static Guid paymentmodeId;
        Establish context = () =>
            {
                config = new MongoConfig();
                payModeService = new PaymentModeService(config);
                payModeQuery = new PaymentModeQuery(config);
                paymentmodeId = Guid.NewGuid();
            };
        Because of = () =>
            {
                PaymentModes paymodes = new PaymentModes
                {
                    _id = paymentmodeId,
                    Name = "Cash",
                    OwnerId = "marthin"
                };
                payModeService.Insert(JsonConvert.SerializeObject(paymodes,Formatting.None),"marthin");
            };
        It should_save_paymentmodes = () =>
            {
                PaymentModes payModes = payModeQuery.Get(paymentmodeId, "marthin");
                payModes.ShouldNotBeNull();
            };
        Cleanup cleanup = () =>
            {
                payModeService.Delete(paymentmodeId);
            };
    }
}
