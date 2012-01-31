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
    public class when_creating_a_invoicepayment : OneEventTestFixture<CreateInvoicePayment, InvoicePaymentCreated>
    {
        public when_creating_a_invoicepayment()
        {
            this.SetupInvoicePaymentFixture();
        }

        protected override CreateInvoicePayment WhenExecuting()
        {
            CreateInvoicePayment cmd =
                new CreateInvoicePayment
                {
                    OwnerId = "oetawan@inforsys.co.id",
                    InvoiceId = Guid.NewGuid(),
                    InvoiceDate = new DateTime(2012, 1, 28),
                    InvoiceNumber = "INV-1",
                    Amount = 10000000
                };
            return cmd;
        }

        [Then]
        public void the_new_invoicepayment_should_have_the_correct_balancedue()
        {
            Assert.That(TheEvent.BalanceDue, Is.EqualTo(10000000m));
        }

        [Then]
        public void the_paidoff_of_new_invoicepayment_should_be_false()
        {
            Assert.That(TheEvent.PaidOff, Is.False);
        }
    }
}