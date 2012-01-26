using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.config;
using dokuku.sales.taxes.service;
using dokuku.sales.taxes.model;
using dokuku.sales.taxes.query;
namespace dokuku.sales.fixture.Tax_Fixture
{
    [Subject("Create Tax")]
    public class When_create_tax
    {
        private static MongoConfig mongo;
        private static IServiceTax serviceTax;
        private static ITaxQueryRepository taxQueryRepo;
        private static Guid taxId;
        Establish context = () =>
            {
                mongo = new MongoConfig();
                serviceTax = new ServiceTax(mongo,null);
                taxQueryRepo = new TaxQueryRepository(mongo);
                taxId = Guid.NewGuid();
            };
        Because of = () =>
            {
                Taxes tax = new Taxes
                {
                    _id = taxId,
                    Name = "PPN",
                    OwnerId = "marthin",
                    Value = 10
                };
                serviceTax.Create(Newtonsoft.Json.JsonConvert.SerializeObject(tax,Newtonsoft.Json.Formatting.None),"marthin");
            };
        It should_return_tax = () =>
            {
                Taxes tax = taxQueryRepo.GetTaxById(taxId, "marthin");
                tax.ShouldNotBeNull();
                IList<Taxes> taxes = taxQueryRepo.GetAllTaxes("marthin").ToList();
                taxes.ShouldNotBeNull();
            };
        Cleanup cleanup = () =>
            {
                serviceTax.Delete(taxId);
            };

    }
}
