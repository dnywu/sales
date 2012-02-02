using System;
using NServiceBus;
using Ncqrs.NServiceBus;
using dokuku.sales.payment.events;
using MongoDB.Driver;
using dokuku.sales.config;
using MongoDB.Bson;
namespace dokuku.sales.payment.denormalizers
{
    public class PaymentRevisedEventHandler : IMessageHandler<EventMessage<PaymentRevised>>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(EventMessage<PaymentRevised> message)
        {
            BsonDocument doc = message.Payload.ToBsonDocument();
            doc["_id"] = message.Payload.PaymentId;
            Collection.Save(doc);
        }

        private MongoCollection Collection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(paymentresource.PaymentReportCollectionName);
            }
        }
    }
}