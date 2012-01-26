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
    public class TaxCreatedHandler : IHandleMessages<TaxCreated>
    {
        public MongoConfig Mongo { get; set; } 
        public void Handle(TaxCreated message)
        {
            BsonDocument tax = BsonDocument.Parse(message.TaxJson);
            BsonDocument index = new BsonDocument();
            index["_id"] = tax["_id"];
            index["Name"] = tax["Name"];
            index["Value"] = tax["Value"];
            index["OwnerId"] = tax["OwnerId"];
            index["Keywords"] = BsonValue.Create(new string[4]
                                    {
                                        tax["_id"].ToString(),
                                        tax["Name"].ToString(),
                                        tax["Value"].ToString(),
                                        tax["OwnerId"].ToString()
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
