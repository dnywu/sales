using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoice.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
namespace dokuku.sales.report.Handlers
{
    public class InvoiceCreatedHandler : IHandleMessages<InvoiceCreated>
    {
        public MongoConfig Mongo { get; set; }
        
        public void Handle(InvoiceCreated message)
        {
            BsonDocument doc = BsonDocument.Parse(message.InvoiceJson);
            BsonDocument index = new BsonDocument();

            index["Keywords"] = BsonValue.Create(new string[13]{
                    doc["_id"].ToString(),
                    doc["Customer"].ToString(),
                    doc["CustomerId"].ToString(),
                    doc["InvoiceDate"].ToString(),
                    doc["DueDate"].ToString(),
                    doc["LateFee"].ToString(),
                    doc["Note"].ToString(),
                    doc["PONo"].ToString(),
                    doc["Status"].ToString(),
                    doc["SubTotal"].ToString(),
                    doc["TermCondition"].ToString(),
                    doc["Total"].ToString(),
                    doc["OwnerId"].ToString()});

            index["InvoiceNo"] = doc["InvoiceNo"];
            index["Customer"] = doc["Customer"];
            index["OwnerId"] = doc["OwnerId"];
            index["_id"] = doc["_id"];
            index["PONo"] = doc["PONo"];
            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }

        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.INVOICE_REPORTS); }
        }
    }
}