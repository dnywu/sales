using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.item.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dokuku.sales.report.Handlers
{
    public class ItemUpdatedHandler : IHandleMessages<ItemUpdated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(ItemUpdated message)
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
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.ITEM_REPORTS); }
        }
    }
}
