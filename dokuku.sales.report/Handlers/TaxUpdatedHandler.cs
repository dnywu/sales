using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
namespace dokuku.sales.report.Handlers
{
    public class TaxUpdatedHandler : IHandleMessages<TaxUpdated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(TaxUpdated message)
        {
            BsonDocument taxUpdated = BsonDocument.Parse(message.TaxUpdatedJson);
            BsonDocument index = new BsonDocument();
            index["_id"] = taxUpdated["_id"];
            index["Name"] = taxUpdated["Name"];
            index["Value"] = taxUpdated["Value"];
            index["OwnerId"] = taxUpdated["OwnerId"];
            index["Keywords"] = BsonValue.Create(new string[4]
                                    {
                                        taxUpdated["_id"].ToString(),
                                        taxUpdated["Name"].ToString(),
                                        taxUpdated["Value"].ToString(),
                                        taxUpdated["OwnerId"].ToString()
                                    });
            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.TAX_REPORTS); }
        }
    }
}
