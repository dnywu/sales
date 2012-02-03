//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NServiceBus;
//using Machine.Specifications;
//using dokuku.sales.invoices.commands;
//using Ncqrs.NServiceBus;

//namespace dokuku.sales.invoices.integrationtest
//{
//    public class When_send_create_invoice_command
//    {
//        static IBus bus;
//        static Guid invoiceId;
//        Establish context = () =>
//        {
//            bus = Configure.With()
//                .Log4Net()
//                .DefaultBuilder()
//                .BinarySerializer()
//                .MsmqTransport()
//                    .IsTransactional(true)
//                    .PurgeOnStartup(false)
//                .MsmqSubscriptionStorage()
//                .UnicastBus()
//                    .LoadMessageHandlers()
//                    .ImpersonateSender(true)
//                .CreateBus()
//                .Start();

//            invoiceId = new Guid("68E153C3-21BA-4714-B69E-0256A1906F4D");
//        };
//        Because of = () =>
//        {
//            bus.Send("dokukuInvoiceDistributorDataBus", new CommandMessage
//            {
//                Payload = new CreateInvoice
//                {
//                    CustomerId = Guid.NewGuid(),
//                    OwnerId = "mart@yahoo.com",
//                    PONo = "123456",
//                    UserName = "marthin"
//                }
//            });
//        };
//        It should_publish_invoice_created_event = () =>
//        {
//        };
//    }
//}
