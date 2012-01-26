using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoice.messages;
using dokuku.sales.payment.messages;
using NServiceBus;
using MongoDB.Bson;
using dokuku.sales.config;
using MongoDB.Driver;
using dokuku.sales.payment.domain;
using MongoDB.Driver.Builders;
namespace dokuku.sales.payment.fixture
{
    [Subject("When send revise invoice payment message")]
    public class When_send_revise_invoice_payment_message
    {
        static Guid paymentId;
        static IBus bus;
        static MongoConfig mongo;
        static BsonDocument doc;
        Establish context = () =>
        {
            mongo = new MongoConfig();
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

            MongoCollection collections = mongo.MongoDatabase.GetCollection("PaymentReports");
            doc = collections.FindOneAs<BsonDocument>(Query.EQ("InvoiceNumber", "INV-1"));
            BsonBinaryData bsonPaymentId = (BsonBinaryData)doc["_id"];
            paymentId = (Guid)bsonPaymentId.RawValue;
        };

        Because of = () =>
            {
                BsonBinaryData invoiceId = (BsonBinaryData)doc["InvoiceId"];
                bus.Send(new RevisePayment()
                {
                    AdjustedPaymentId = paymentId,
                    AmountPaid = 8000000,
                    BankCharge = 0,
                    InvoiceId = (Guid)invoiceId.RawValue,
                    InvoiceNo = "INV-1",
                    Notes = doc["PRNotes"].ToString(),
                    PaymentDate = (DateTime)doc["PaymentDate"],
                    PaymentModeId = Guid.NewGuid(),
                    Reference = doc["PRReference"].ToString()
                });
            };

        It should_send_revice_payment_message = () =>
            {

            };
    }
}