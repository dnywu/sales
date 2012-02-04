using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.domain;
using NUnit.Framework;

namespace dokuku.sales.invoices.fixture
{
    [Specification]
    public class When_add_item_with_discount : OneEventTestFixture<AddInvoiceItem, InvoiceItemAdded>
    {
        Guid _customerId;
        Guid _invoiceItemId;
        Guid _taxId;
        public When_add_item_with_discount()
        {
            this.SetupInvoiceFixture();
            _customerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC");
            _invoiceItemId = new Guid("6D810987-F3A1-4826-8AFE-294B64C097F0");
            _taxId = new Guid("2BA65A98-FFB3-4D1B-85B9-A5B6F6736B22");            
        }
        protected override IEnumerable<object> GivenEvents()
        {
            return new List<InvoiceCreated>() { 
                new InvoiceCreated
                {
                    InvoiceId = EventSourceId,
                    CustomerId = _customerId,
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
                }
            };
        }
        protected override AddInvoiceItem WhenExecuting()
        {
            return new AddInvoiceItem
            {
                OwnerId = "mart@y.c",
                UserName = "marthin",
                InvoiceId = EventSourceId,
                ItemId = _invoiceItemId,
                Description = "untuk beli hp samsung",
                Quantity = 10,
                Price = 500000,
                DiscountInPercent = 10,
                TaxId = _taxId
            };
        }

        [Then]
        public void should_have_the_correct_item_event()
        {
            Assert.That(TheEvent, Is.EqualTo(new InvoiceItemAdded
            {
                OwnerId = "mart@y.c",
                UserName = "marthin",
                InvoiceId = EventSourceId,
                ItemId = _invoiceItemId,
                Description = "untuk beli hp samsung",
                Quantity = 10,
                Price = 500000,
                DiscountInPercent = 10,
                TaxCode = "PPn",
                Summary = new Summary()
                {
                    SubTotal = 4500000,
                    DiscountTotal = 0,
                    NetTotal = 4500000,
                    Taxes = new Tax[] { }
                },
                Total = 4500000
            }));
        }
    }
}
