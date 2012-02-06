//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Machine.Specifications;
//using dokuku.sales.invoices.service;
//using dokuku.sales.config;
//using dokuku.sales.invoices.model;
//using MongoDB.Driver.Builders;
//namespace dokuku.sales.fixture
//{
//    [Subject("Generate invoice number")]
//    public class When_generate_invoice_number
//    {
//        static InvoiceAutoNumberGenerator gen;
//        static string result="";
//        Establish context = () =>
//        {
//            gen = new InvoiceAutoNumberGenerator(new MongoConfig());
//        };

//        Because of = () =>
//        {
//            gen.GenerateInvoiceNumber(new DateTime(2012, 1, 17), "oetawan@gmail.com");
//            result = gen.GenerateInvoiceNumber(new DateTime(2012, 1, 17), "oetawan@gmail.com");
//        };

//        It should_generate_invoice_number = () =>
//        {
//            result.ShouldEqual("INV-2");
//        };

//        Cleanup cleanup = () =>
//        {
//            new MongoConfig().MongoDatabase.GetCollection<InvoiceAutoNumberConfig>(typeof(InvoiceAutoNumberConfig).Name).Remove(Query.And(Query.EQ("_id", typeof(InvoiceAutoNumberConfig).Name), Query.EQ("CompanyId", "oetawan@gmail.com")));
//            new MongoConfig().MongoDatabase.GetCollection<InvoiceAutoNumberDefault>(typeof(InvoiceAutoNumberDefault).Name).Remove(Query.And(Query.EQ("_id", typeof(InvoiceAutoNumberDefault).Name),  Query.EQ("CompanyId", "oetawan@gmail.com")));
//        };
//    }
//}