using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.currency.messages;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace dokuku.sales.report.Handlers
{
    public class CurrenciesDeletedHandler : IHandleMessages<CurrenciesDeleted>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(CurrenciesDeleted message)
        {
            Collections.Remove(Query.EQ("_id", message.Id));
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.CURRENCY_REPORTS); }
        }
    }
}
