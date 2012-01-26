using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.messages;
using NServiceBus;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.report.Handlers
{
    public class CustomerDeletedHandler : IHandleMessages<CustomerDeleted>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(CustomerDeleted message)
        {
            Collections.Remove(Query.EQ("_id",message.Id));
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(CollectionName.CUSTOMER_REPORTS); }
        }
    }
}
