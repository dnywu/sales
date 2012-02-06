//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Machine.Specifications;
//using NServiceBus;
//using dokuku.sales.invoice.messages;
//using dokuku.sales.invoices.model;
//using MongoDB.Bson;
//namespace dokuku.sales.fixture
//{
//    [Subject("When publich invoice approved")]
//    class When_publich_invoice_approved
//    {
//        static IBus bus;
//        Establish context = () =>
//            {
//                bus = Configure.With()
//                    .Log4Net()
//                    .DefaultBuilder()
//                    .BinarySerializer()
//                    .MsmqTransport()
//                        .IsTransactional(true)
//                        .PurgeOnStartup(false)
//                    .MsmqSubscriptionStorage()
//                    .UnicastBus()
//                        .LoadMessageHandlers()
//                        .ImpersonateSender(true)
//                    .CreateBus()
//                    .Start();
//            };
        
//        Because of = () =>
//            {
//                Invoices invoice = new Invoices()
//                {
//                    _id = Guid.NewGuid(),
//                    Currency = "IDR",
//                    Customer = "Matahari",
//                    CustomerId = Guid.NewGuid().ToString(),
//                    DueDate = DateTime.Today,
//                    ExchangeRate = 1,
//                    InvoiceDate = DateTime.Today,
//                    InvoiceNo = "INV-1",
//                    LateFee = "",
//                    Note = "",
//                    OwnerId = "oetawan@inforsys.co.id",
//                    PONo = "",
//                    SubTotal = 10000000,
//                    TermCondition = "",
//                    Terms = new Term() { Name = "ASAP", Value = 0 },
//                    Total = 10000000,
//                    Items = new InvoiceItem[1] { new InvoiceItem{ 
//                        Amount = 10000000, 
//                        Description = "Macbook Pro", 
//                        Discount = 0, 
//                        ItemId = Guid.NewGuid(),
//                        PartName = "Macbook Pro", 
//                        Qty = 1,
//                        Rate = 10000000}}
//                };
//                bus.Publish(new InvoiceApproved { InvoiceJson = invoice.ToJson() });
//            };

//        It should_publish_invoice_approved = () => { };
//    }
//}