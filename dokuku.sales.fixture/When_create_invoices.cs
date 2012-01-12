using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices;

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
            ciRepo = new InvoicesRepository();
            id = Guid.NewGuid();
        };

        Because of = () =>
            {
                ciRepo.Save(new Invoices()
                {
                    _id = id,
                    OwnerId = "oetawan@inforsys.co.id",
                    Customer="Randi",
                    InvoiceNo="Invoice-00001",
                    InvoiceDate= DateTime.Today,
                    Terms="15 of Days",
                    DueDate = DateTime.Today,
                    LateFee = "No Late Fee",
                    Note = "catatan ci Invoces",
                    TermCondition="Not Good",
                    SubTotal=5000,
                    Total=5000,
                    Items=




                    //OwnerId = "oetawan@inforsys.co.id",
                    //BillingAddress = "Seipana",
                    //City = "Batam",
                    //Currency = "IDR",
                    //Email = "oetawan.ac@gmail.com",
                    //Salutation = "Mr. ",
                    //FirstName = "Oet",
                    //LastName = "Chandra",
                    //Fax = "472111",
                    //MobilePhone = "082173739678",
                    //Name = "Bulan bintang",
                    //Phone = "0778472111",
                    //PostalCode = "29432",
                    //State = "Kepri"
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
