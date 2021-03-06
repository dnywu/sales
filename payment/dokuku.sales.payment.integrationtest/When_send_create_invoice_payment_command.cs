﻿using System;
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
                Payload = new CreateInvoicePayment
                {
                    InvoiceId = invoiceId,
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