using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.messages;
using NServiceBus;
using MongoDB.Driver;
using dokuku.sales.config;
using MongoDB.Bson;

namespace dokuku.sales.report.Handlers
{
    public class CustomerUpdatedHandler : IHandleMessages<CustomerUpdated>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(CustomerUpdated message)
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
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.CUSTOMER_REPORTS); }
        }
    }
}
