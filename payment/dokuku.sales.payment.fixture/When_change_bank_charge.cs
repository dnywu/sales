using System;
using System.Collections.Generic;
using Ncqrs.Spec;
using dokuku.sales.payment.events;
using dokuku.sales.payment.commands;
using dokuku.sales.payment.common;
using NUnit.Framework;
namespace dokuku.sales.payment.fixture
{
    [Specification]
    public class When_change_bank_charge : OneEventTestFixture<ChangeBankCharge, BankChargeChanged>
    {
        static Guid paymentId = Guid.NewGuid();
        public When_change_bank_charge()
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
                    PaymentMode = new PaymentMode{ Id = Guid.NewGuid() },
                    Reference = "123",
                    Notes = "Revise payment test",
                    BalanceDue = 5000000,
                    PaidOff = false
                }
            };
        }

        protected override ChangeBankCharge WhenExecuting()
        {
            return new ChangeBankCharge
            {
                InvoiceId = this.EventSourceId,
                PaymentId = paymentId,
                BankCharge = 1000000
            };
        }

        [Then]
        public void the_invoice_payment_should_have_the_correct_bank_charge()
        {
            Assert.That(TheEvent.BankCharge, Is.EqualTo(1000000m));
        }
    }
}