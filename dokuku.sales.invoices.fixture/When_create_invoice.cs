using System;
using Ncqrs.Spec;
using NUnit.Framework;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.common;
using dokuku.sales.invoices.domain;
using Ncqrs.Spec.Fakes;
namespace dokuku.sales.invoices.fixture
{
    [Specification]
    public class When_create_invoice : OneEventTestFixture<CreateInvoice, InvoiceCreated>
    {
        Guid invoiceid;
        public When_create_invoice()
        {
            this.SetupInvoiceFixture();
            invoiceid = Guid.NewGuid();
        }

        protected override void RegisterFakesInConfiguration(EnvironmentConfigurationWrapper configuration)
        {
            var autoNumber = new FakeInvoiceNumberGenerator();
            configuration.Register<IInvoiceAutoNumberGenerator>(autoNumber);
        }

        protected override CreateInvoice WhenExecuting()
        {
            CreateInvoice cmd =
                new CreateInvoice
                {
                    InvoiceId = invoiceid,
                    Customer = new Customer(Guid.NewGuid(),"mart@yahoo.com","marthin","seipanas","sei panas batam"),
                    PONo = "",
                    InvoiceDate = DateTime.Now,
                    Terms = new Term("14 hari", 14m),
                    DueDate = DateTime.Now.AddDays(14),
                    Note = "catatan",
                    ExchangeRate = 9000m,
                    BaseCcy = "IDR",
                    Currency = "IDR",
                    SubTotal = 1500000m,
                    Total = 1500000m,
                    Items = new InvoiceItem[] { new InvoiceItem() { ItemId = Guid.NewGuid() } },
                    OwnerId = "owner@yahoo.com",
                    UserName = "marthin",
                    TermCondition = "belum lunas"
                };
            return cmd;
        }

        [Then]
        public void should_equal_id()
        {
            Assert.That(TheEvent.InvoiceNo, Is.EqualTo("DRAFT-1"));
        }
    }
}
