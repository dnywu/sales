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
    [Subject("Send pay invoice command")]
    public class When_send_pay_invoice_command
    {
        static IBus bus;
        static Guid invoiceId;
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

            invoiceId = new Guid("68E153C3-21BA-4714-B69E-0256A1906F4D");
        };

        Because of = () =>
        {
            bus.Send("dokukuPaymentDistributorDataBus", new CommandMessage
            {
                Payload = new PayInvoice
                {
                    InvoiceId = invoiceId,
                    PaymentId = Guid.NewGuid(),
                    AmountPaid = 5000000,
                    BankCharge = 0,
                    PaymentDate = DateTime.Now.Date,
                    PaymentMode = Guid.NewGuid(),
                    Reference = "123",
                    Notes = "First payment"
                }
            });
        };

        It should_publish_invoicepaid_event = () =>
        {
        };
    }
}