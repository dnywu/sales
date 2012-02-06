using System;
using Ncqrs.Spec;
using NUnit.Framework;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.domain;
using Ncqrs.Spec.Fakes;
using dokuku.sales.invoice.services;
using Ncqrs;
using Ncqrs.Commanding.CommandExecution;
using dokuku.sales.invoices.repository;
namespace dokuku.sales.invoices.fixture
{
    [Specification]
    public class When_create_invoice : OneEventTestFixture<CreateInvoice, InvoiceCreated>
    {
        public When_create_invoice()
        {
            this.SetupInvoiceFixture();
        }

        protected override void RegisterFakesInConfiguration(EnvironmentConfigurationWrapper configuration)
        {
            configuration.Register<IInvoiceAutoNumberGenerator>(new FakeInvoiceNumberGenerator());
            configuration.Register<ICustomerRepository>(new FakeCustomerRepository());
            configuration.Register<IOrganizationRepository>(new FakeOrganizationRepository());
            configuration.Register<IExchangeRateRepository>(new FakeExchangeRateRepository());
        }

        protected override CreateInvoice WhenExecuting()
        {
            return new CreateInvoice
            {
                InvoiceId = new Guid("AD4DB777-3329-46E9-9712-04465DED0722"),
                CustomerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC"),
                PONumber = "PO-001",
                OwnerId = "mart@y.c",
                UserName = "marthin"
            };
        }

        [Then]
        public void invoice_should_have_the_invoice_created_event()
        {
            Assert.That(TheEvent, Is.EqualTo(new InvoiceCreated
            {
                InvoiceId = new Guid("AD4DB777-3329-46E9-9712-04465DED0722"),
                CustomerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC"),
                DecimalPlace = 2,
                BaseCurrency = "IDR",
                PONo = "PO-001",
                OwnerId = "mart@y.c",
                UserName = "marthin",
                Status = InvoiceStatus.DRAFT,
                TermCode = "001",
                InvoiceDate = DateTime.Now.Date,
                DueDate = DateTime.Now.Date.AddDays(7),
                InvoiceNo = "DRAFT-1",
                Rate = 1,
                TransactionCurrency = "IDR"
            }));
        }
    }
}