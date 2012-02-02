using System;
using Ncqrs.Spec;
using NUnit.Framework;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.common;
using dokuku.sales.invoices.domain;
using Ncqrs.Spec.Fakes;
using System.Collections.Generic;
namespace dokuku.sales.invoices.fixture
{
    [Specification]
    public class When_update_invoice : OneEventTestFixture<UpdateInvoice, InvoiceUpdated>
    {
        Guid invoiceid;
        public When_update_invoice()
        {
            this.SetupInvoiceFixture();
            invoiceid = Guid.NewGuid();
        }

        protected override IEnumerable<object> GivenEvents()
        {
            return new List<InvoiceCreated>
            {
                new InvoiceCreated{
                    InvoiceNo = "DRAFT-1",
                    OwnerId = "marthin",
                    InvoiceId = EventSourceId
                }
            };
        }

        protected override UpdateInvoice WhenExecuting()
        {
            UpdateInvoice cmd = new UpdateInvoice
            {
                InvoiceId = EventSourceId,
                OwnerId = "marthin",
                UserName = "marthin"
            };
                
            return cmd;
        }

        [Then]
        public void should_equal_id()
        {
            Assert.That(TheEvent.UserName, Is.EqualTo("marthin"));
            Assert.That(TheEvent.InvoiceNo, Is.EqualTo("DRAFT-1"));
        }
    }
}