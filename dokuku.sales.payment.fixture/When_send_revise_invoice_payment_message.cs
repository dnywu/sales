using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoice.messages;
using dokuku.sales.payment.messages;
using NServiceBus;
using MongoDB.Bson;
namespace dokuku.sales.payment.fixture
{
    [Subject("When send revise invoice payment message")]
    public class When_send_revise_invoice_payment_message
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
                Total = 10000000
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

        Because of = () =>
            {
                bus.Send(new RevisePayment()
                {
                });
            };
    }
}
