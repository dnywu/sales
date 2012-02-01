using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using Ncqrs.Spec.Fakes;
using NUnit.Framework;
using dokuku.sales.payment.commands;
using dokuku.sales.payment.events;
using Ncqrs.Config.StructureMap;
using Ncqrs;
using StructureMap;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.CommandService.Infrastructure;
using dokuku.sales.payment.common;
namespace dokuku.sales.payment.fixture
{
    [Specification]
    public class when_pay_invoice_full : OneEventTestFixture<PayInvoice, InvoicePaid>
    {
        public when_pay_invoice_full()
        {
            this.SetupInvoicePaymentFixture();
        }

        protected override IEnumerable<object> GivenEvents()
        {
            return new List<InvoicePaymentCreated>
            {
                new InvoicePaymentCreated{
                    InvoiceId = EventSourceId,
                    OwnerId = "oetawan@inforsys.co.id",
                    InvoiceNumber = "INV-1",
                    InvoiceDate = new DateTime(2012,1,28),
                    Amount = 10000000,
                    BalanceDue = 10000000,
                    PaidOff = false
                }
            };
        }

        protected override PayInvoice WhenExecuting()
        {
            return new PayInvoice
            {
                InvoiceId = EventSourceId,
                AmountPaid = 10000000,
                BankCharge = 0,
                PaymentDate = new DateTime(2012, 1, 28),
                PaymentMode = new PaymentMode { Id = Guid.NewGuid() },
                Reference = "",
                Notes = ""
            };
        }

        [Then]
        public void the_invoice_payment_balancedue_should_be_zero()
        {
            Assert.That(TheEvent.BalanceDue, Is.EqualTo(0));
        }

        [Then]
        public void the_paidoff_of_new_invoicepayment_should_be_true()
        {
            Assert.That(TheEvent.PaidOff, Is.True);
        }
    }
}