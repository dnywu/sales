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
namespace dokuku.sales.payment.fixture
{
    [Specification]
    public class when_revise_payment : OneEventTestFixture<RevisePayment, PaymentRevised>
    {
        static Guid paymentId = Guid.NewGuid();
        public when_revise_payment()
        {
            this.SetupInvoicePaymentFixture();
        }

        protected override IEnumerable<object> GivenEvents()
        {
            return new List<Object>
            {
                new InvoicePaymentCreated{
                    InvoiceId = EventSourceId,
                    OwnerId = "oetawan@inforsys.co.id",
                    InvoiceNumber = "INV-1",
                    InvoiceDate = new DateTime(2012,1,28),
                    Amount = 10000000,
                    BalanceDue = 10000000,
                    PaidOff = false
                },
                new InvoicePaid{
                    OwnerId = "oetawan@inforsys.co.id",
                    InvoiceId = EventSourceId,
                    PaymentId = paymentId,
                    AmountPaid = 5000000,
                    BankCharge = 0,
                    PaymentDate = new DateTime(2012,1,31),
                    PaymentMode = Guid.NewGuid(),
                    Reference = "123",
                    Notes = "Revise payment test",
                    BalanceDue = 5000000,
                    PaidOff = false
                }
            };
        }

        protected override RevisePayment WhenExecuting()
        {
            return new RevisePayment
            {
                InvoiceId = EventSourceId,
                PaymentId = paymentId,
                AmountPaid = 10000000,
                BankCharge = 0,
                PaymentDate = new DateTime(2012, 1, 28),
                PaymentMode = Guid.NewGuid(),
                Reference = "",
                Notes = ""
            };
        }

        [Then]
        public void the_invoice_payment_should_have_the_correct_balancedue()
        {
            Assert.That(TheEvent.BalanceDue, Is.EqualTo(0m));
        }

        [Then]
        public void the_invoice_payment_should_have_the_correct_paidoff()
        {
            Assert.That(TheEvent.PaidOff, Is.True);
        }
    }
}