using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.currency.report;
using dokuku.sales.currency.service;
using dokuku.sales.config;
using dokuku.sales.currency.model;

namespace dokuku.sales.fixture.currency
{
    [Subject("Create Currency")]
    public class When_create_currency
    {
        private static MongoConfig mongo;
        private static ICurrencyService serviceTax;
        private static ICurrencyQueryRepository taxQueryRepo;
        private static Guid ccyId;
        Establish context = () =>
            {
                mongo = new MongoConfig();
                serviceTax = new CurrencyService(mongo, null);
                taxQueryRepo = new CurrencyQueryRepository(mongo);
                ccyId = Guid.NewGuid();
            };
        Because of = () =>
        {
            Currencies ccy = new Currencies
            {
                _id = ccyId,
                Name = "PPN",
                OwnerId = "marthin",
                Code = "IDR",
                Rounding = 0
            };
            serviceTax.Create(Newtonsoft.Json.JsonConvert.SerializeObject(ccy, Newtonsoft.Json.Formatting.None), "marthin");
        };
        It should_return_currency = () =>
        {
            IEnumerable<Currencies> ccy = taxQueryRepo.GetAllCurrency("marthin");
            ccy.Count().Equals(1);
        };
        Cleanup cleanup = () =>
        {
            serviceTax.Delete(ccyId);
        };
    }
}
