using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
namespace dokuku.sales.report.Handlers
{
    public class TaxDeletedHandler : IHandleMessages<TaxDeleted>
    {
        public MongoConfig Mongo { get; set; } 
        public void Handle(TaxDeleted message)
        {
            Collections.Remove(Query.EQ("_id", message.Id));
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.TAX_REPORTS); }
        }
    }
}
