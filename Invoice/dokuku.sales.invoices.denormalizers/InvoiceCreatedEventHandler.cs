using System;
using NServiceBus;
using Ncqrs.NServiceBus;
using dokuku.sales.invoices.events;
using MongoDB.Driver;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace dokuku.sales.invoices.denormalizers
{
    class InvoiceCreatedEventHandler : IMessageHandler<EventMessage<InvoiceCreated>>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(EventMessage<InvoiceCreated> message)
        {
            BsonDocument doc = message.Payload.ToBsonDocument();
            doc["_id"] = message.Payload.InvoiceId;
            Collection.Save(doc);
            CreateIndex(message);
        }

        private void CreateIndex(EventMessage<InvoiceCreated> message)
        {
            //BsonDocument doc = message.Payload.ToBsonDocument();
            BsonDocument index = new BsonDocument();

            index["Keywords"] = BsonValue.Create(new string[]{
                    message.Payload.InvoiceId == null ? "" : message.Payload.InvoiceId.ToString(),
                    message.Payload.InvoiceNo == null ? "" : message.Payload.InvoiceNo,
                    message.Payload.PONo == null ? "" : message.Payload.PONo,
                    message.Payload.Customer == null ? "" : message.Payload.Customer.Name,
                    message.Payload.UserName == null ? "" : message.Payload.UserName,
                    message.Payload.OwnerId == null ? "" : message.Payload.OwnerId});

            index["InvoiceNo"] = BsonValue.Create(message.Payload.InvoiceNo == null ? "" : message.Payload.InvoiceNo);
            index["Customer"] = BsonValue.Create(message.Payload.Customer == null ? "" : message.Payload.Customer.Name);
            index["OwnerId"] = BsonValue.Create(message.Payload.OwnerId == null ? "" : message.Payload.OwnerId);
            index["_id"] = BsonValue.Create(message.Payload.InvoiceId == null ? "" : message.Payload.InvoiceId.ToString());
            index["PONo"] = BsonValue.Create(message.Payload.PONo == null ? "" : message.Payload.PONo);
            IndexCollection.Save(index);
            IndexCollection.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(invoiceresources.InvoiceReportCollectionName);
            }
        }
        private MongoCollection IndexCollection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(invoiceresources.InvoiceIndexCollectionName);
            }
        }
    }
}
