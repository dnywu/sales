using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using NServiceBus;
using dokuku.sales.payment.commands;
using Ncqrs.NServiceBus;
namespace dokuku.sales.payment.integrationtest
{
    [Subject("Send create invoice payment command")]
    public class When_send_create_invoice_payment_command
    {
        static IBus bus;
        Establish context = () =>
        {
            bus = Configure.With()
                .Log4Net()
                .DefaultBuilder()
                .BinarySerializer()
                .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(false)
                .MsmqSubscriptionStorage()
                .UnicastBus()
                    .LoadMessageHandlers()
                    .ImpersonateSender(true)
                .CreateBus()
                .Start();
        };

        Because of = () =>
        {
            bus.Send("dokukuPaymentDistributorDataBus", new CommandMessage
            {
                Payload = new CreateInvoicePayment
                {
                    InvoiceId = Guid.NewGuid(),
                    InvoiceNumber = "INV-1",
                    InvoiceDate = new DateTime(2012, 1, 30),
                    OwnerId = "oetawan@inforsys.co.id",
                    Amount = 10000000
                }
            });
        };

        It should_publish_invoicepayment_created_event = () =>
        {
        };
    }
}