﻿using System;
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

            index["Keywords"] = BsonValue.Create(new string[12]{
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
                    doc["Total"].ToString()});

            index["InvoiceNumber"] = doc["_id"].ToString();
            index["Customer"] = doc["Customer"].ToString();

            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }

        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.INVOICE_REPORTS); }
        }
    }
}