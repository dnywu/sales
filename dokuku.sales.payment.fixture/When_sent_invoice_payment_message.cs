using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using NServiceBus;
using MongoDB.Bson;
using System.Dynamic;
using dokuku.sales.invoice.messages;
using dokuku.sales.payment.messages;
namespace dokuku.sales.payment.fixture
{
    [Subject("Send invoice payment message")]
    class When_sent_invoice_payment_message
    {
        static Guid invoiceId;
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
                invoiceId = Guid.NewGuid();
            };
        
        Because of = () =>
            {
                Object invoice = new
                {
                    _id = invoiceId,
                    Currency = "IDR",
                    Customer = "Matahari",
                    CustomerId = Guid.NewGuid().ToString(),
                    DueDate = DateTime.Today,
                    ExchangeRate = 1,
                    InvoiceDate = DateTime.Today,
                    InvoiceNo = "INV-1",
                    LateFee = "",
                    Note = "",
                    OwnerId = "oetawan@inforsys.co.id",
                    PONo = "",
                    SubTotal = 10000000,
                    TermCondition = "",
                    Total = 10000000,
                    Status = "Draft"
                };
                bus.Publish(new InvoiceApproved { InvoiceJson = invoice.ToJson() });
                System.Threading.Thread.Sleep(3000);
                bus.Send(new PayInvoice()
                    {
                        InvoiceId = invoiceId,
                        InvoiceNo = "INV-1",
                        AmountPaid = 5000000,
                        BankCharge = 0,
                        PaymentDate = DateTime.Now,
                        PaymentModeId = Guid.NewGuid(),
                        Reference = "test 1",
                        Notes = "test"
                    });
            };

        It should_send_pay_invoice_message = () => { };
    }
}