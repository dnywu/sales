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
        public When_add_item_with_discount()
        {
            this.SetupInvoiceFixture();
            _customerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC");
            _invoiceItemId = new Guid("6D810987-F3A1-4826-8AFE-294B64C097F0");
        }
        protected override IEnumerable<object> GivenEvents()
        {
            return new GivenEvents_InvoiceCreated().Events;
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
                TaxCode = "NONE"
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
                DiscountAmount = 500000,
                TaxCode = "NONE",
                Summary = new Summary()
                {
                    SubTotal = 4500000,
                    DiscountTotal = 0,
                    NetTotal = 4500000,
                    Taxes = new TaxSummary[1] { new TaxSummary{TaxCode="NONE", TaxAmount=0} }
                },
                Total = 4500000,
                ItemNumber = 1
            }));
        }
    }
}