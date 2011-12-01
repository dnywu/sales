using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.item.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.report.Handlers
{
    public class ItemDeletedHandler : IHandleMessages<ItemDeleted>
    {
        public MongoConfig Mongo { get; set; } 
        public void Handle(ItemDeleted message)
        {
            Collections.Remove(Query.EQ("_id", message.Id));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.ITEM_REPORTS); }
        }
    }
}
