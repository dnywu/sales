using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.model;
using dokuku.sales.invoices.query;
using dokuku.sales.config;

namespace dokuku.sales.fixture
{
    [Subject("Searching invoices")]
    public class When_search_invoices
    {
        private static IInvoicesRepository invRepo;
        private static IInvoicesQueryRepository qryRepo;
        private static IEnumerable<InvoiceReports> result;

        Establish context = () =>
        {
            MongoConfig mongo = new MongoConfig();
            invRepo = new InvoicesRepository(mongo);
            qryRepo = new InvoicesQueryRepository(mongo);

            invRepo.Save(new Invoices()
                {
                    _id = "INV-1",
                    OwnerId = "oetawan@inforsys.co.id",
                    Customer = "Randi",
                    InvoiceNo = "Inv-01",
                    InvoiceDate = DateTime.Today,
                    Terms = "15 of Days",
                    DueDate = DateTime.Today,
                    LateFee = "No Late Fee",
                    Note = "catatan ci Invoces",
                    TermCondition = "Not Good",
                    SubTotal = 5000,
                    Total = 5000,
                    Items = new InvoiceItems[2] {
                        new InvoiceItems(){ PartName= "partTest", Description="Testing", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500},
                        new InvoiceItems(){ PartName= "partTest2", Description="Testing2", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500}}
                });
            invRepo.Save(new Invoices()
                {
                    _id = "INV-2",
                    OwnerId = "oetawan@inforsys.co.id",
                    Customer = "Finsa",
                    InvoiceNo = "Inv-01",
                    InvoiceDate = DateTime.Today,
                    Terms = "15 of Days",
                    DueDate = DateTime.Today,
                    LateFee = "No Late Fee",
                    Note = "catatan ci Invoces",
                    TermCondition = "Not Good",
                    SubTotal = 5000,
                    Total = 5000,
                    Items = new InvoiceItems[2] {
                        new InvoiceItems(){ PartName= "partTest", Description="Testing", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500},
                        new InvoiceItems(){ PartName= "partTest2", Description="Testing2", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500}}
                });
            invRepo.Save(new Invoices()
                {
                    _id = "INV-3",
                    OwnerId = "oetawan@inforsys.co.id",
                    Customer = "Michel",
                    InvoiceNo = "Inv-01",
                    InvoiceDate = DateTime.Today,
                    Terms = "25 of Days",
                    DueDate = DateTime.Today,
                    LateFee = "No Late Fee",
                    Note = "catatan ci Invoces",
                    TermCondition = "Not Good",
                    SubTotal = 5000,
                    Total = 5000,
                    Items = new InvoiceItems[2] {
                        new InvoiceItems(){ PartName= "partTest", Description="Testing", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500},
                        new InvoiceItems(){ PartName= "partTest2", Description="Testing2", Qty=1, Rate= 2500, Discount= 0, Tax=0, Amount=2500}}
                });
        };

        Because of = () =>
            {
                result = qryRepo.Search("oetawan@inforsys.co.id", new string[1] { "15 of Days" });
            };

        It should_return_invoices_with_that_keywords = () =>
        {
            result.Count().ShouldEqual(2);
        };

        Cleanup cleanup = () =>
        {
            invRepo.Delete("INV-1", "oetawan@inforsys.co.id");
            invRepo.Delete("INV-2", "oetawan@inforsys.co.id");
            invRepo.Delete("INV-3", "oetawan@inforsys.co.id");
        };
    }
}