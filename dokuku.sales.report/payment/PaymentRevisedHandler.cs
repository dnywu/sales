using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.payment.events;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
namespace dokuku.sales.report.Handlers
{
    public class PaymentRevisedHandler : IHandleMessages<PaymentRevised>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(PaymentRevised message)
        {
            BsonDocument doc = BsonDocument.Parse(message.PaymentJson);
            doc["_id"] = doc["PaymentRecordId"];
            Collections.Save(doc);
        }

        private MongoCollection Collections
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(CollectionName.PAYMENT_REPORTS);
            }
        }
    }
}