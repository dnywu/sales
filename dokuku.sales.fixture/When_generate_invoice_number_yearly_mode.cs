using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices.service;
using dokuku.sales.config;
using dokuku.sales.invoices.model;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
namespace dokuku.sales.fixture
{
    [Subject("Generate invoice number")]
    public class When_generate_invoice_number_yearly_mode
    {
        static MongoCollection<InvoiceAutoNumberConfig> configCol;
        static MongoCollection<InvoiceAutoNumberYearly> autoNumberCol;
        static InvoiceAutoNumberGenerator gen;
        static string result="";
        Establish context = () =>
        {
            MongoConfig cfg = new MongoConfig();
            configCol = cfg.MongoDatabase.GetCollection<InvoiceAutoNumberConfig>(typeof(InvoiceAutoNumberConfig).Name);
            autoNumberCol = cfg.MongoDatabase.GetCollection<InvoiceAutoNumberYearly>(typeof(InvoiceAutoNumberYearly).Name);
            gen = new InvoiceAutoNumberGenerator(cfg);
            configCol.Save<InvoiceAutoNumberConfig>(new InvoiceAutoNumberConfig(typeof(InvoiceAutoNumberConfig).Name, AutoNumberMode.Yearly, "INV-", "oetawan@gmail.com"));
        };

        Because of = () =>
        {
            gen.GenerateInvoiceNumber(new DateTime(2012, 1, 17), "oetawan@gmail.com");
            result = gen.GenerateInvoiceNumber(new DateTime(2012, 1, 18), "oetawan@gmail.com");
        };

        It should_generate_invoice_number = () =>
        {
            result.ShouldEqual("INV-20120000002");
        };

        Cleanup cleanup = () =>
        {
            configCol.Remove(Query.And(Query.EQ("_id", typeof(InvoiceAutoNumberConfig).Name), Query.EQ("CompanyId", "oetawan@gmail.com")));
            autoNumberCol.Remove(Query.And(Query.EQ("_id", "2012"), Query.EQ("CompanyId", "oetawan@gmail.com")));
        };
    }
}