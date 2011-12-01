using System;
using dokuku.sales.item.messages;
using NServiceBus;
using System.Threading;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
namespace dokuku.sales.report.Handlers
{
    public class ItemCreatedHandler : IHandleMessages<ItemCreated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(ItemCreated message)
        {
            BsonDocument doc = BsonDocument.Parse(message.Data);
            BsonDocument index = new BsonDocument();
            index["Keywords"] = BsonValue.Create(new string[5] 
            {
                doc["_id"].ToString(),
                doc["OwnerId"].ToString(),
                doc["Code"].ToString(),
                doc["Barcode"].ToString(),
                doc["Name"].ToString()
            });
            index["_id"] = doc["_id"];
            index["OwnerId"] = doc["OwnerId"];
            index["Code"] = doc["Code"];
            index["Barcode"] = doc["Barcode"];
            index["Name"] = doc["Name"];
            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.ITEM_REPORTS); }
        }
    }
}