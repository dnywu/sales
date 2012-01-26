using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
namespace dokuku.sales.report.Handlers
{
    public class CustomerCreatedHandler : IHandleMessages<CustomerCreated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(CustomerCreated message)
        {
            BsonDocument doc = BsonDocument.Parse(message.Data);
            BsonDocument index = new BsonDocument();
            index["Keywords"] = BsonValue.Create(new string[5] 
            {
                doc["_id"].ToString(),
                doc["OwnerId"].ToString(),
                doc["Name"].ToString(),
                doc["Email"].ToString(),
                doc["BillingAddress"].ToString()
            });
            index["_id"] = doc["_id"];
            index["OwnerId"] = doc["OwnerId"];
            index["Name"] = doc["Name"];
            index["Email"] = doc["Email"];
            index["BillingAddress"] = doc["BillingAddress"];
            Collections.Save(index);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.CUSTOMER_REPORTS); }
        }
    }
}
