using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.currency.messages;
using NServiceBus;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.config;

namespace dokuku.sales.report.Handlers
{
    public class CurrenciesCreatedHandler : IHandleMessages<CurrenciesCreated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(CurrenciesCreated message)
        {
            BsonDocument ccy = BsonDocument.Parse(message.CurrenciesCreatedJson);
            BsonDocument index = new BsonDocument();
            index["_id"] = ccy["_id"];
            index["Name"] = ccy["Name"];
            index["Code"] = ccy["Code"];
            index["OwnerId"] = ccy["OwnerId"];
            index["Keywords"] = BsonValue.Create(new string[] {
                                    ccy["_id"].ToString(),
                                    ccy["Name"].ToString(),
                                    ccy["Code"].ToString(),
                                    ccy["OwnerId"].ToString()
            });
            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.CURRENCY_REPORTS); }
        }
    }
}
