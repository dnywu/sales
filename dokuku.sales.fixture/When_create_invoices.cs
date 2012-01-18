using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.model;

namespace dokuku.sales.fixture
{
    [Subject("Creating invoices")]
    class When_create_invoices
    {
        private static IInvoicesRepository ciRepo;
        private static Invoices ci;
        private static Guid id;

        Establish context = () =>
        {
            ciRepo = new InvoicesRepository(new config.MongoConfig());
            id = Guid.NewGuid();
        };

        Because of = () =>
            {
                ciRepo.Save(new Invoices()
                {
                    _id = id,
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
            };

        It should_create_organization = () =>
        {
            ci = ciRepo.Get(id);
            ci.ShouldNotBeNull();
        };

        Cleanup cleanup = () =>
        {
            ciRepo.Delete(id);
        };

    }
}
