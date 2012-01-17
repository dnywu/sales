using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using dokuku.sales.customer.model;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace dokuku.sales.customer.repository
{
    public class CustomerReportRepository : ICustomerReportRepository
    {
        MongoConfig mongo;
        public CustomerReportRepository(MongoConfig mongo)
        {
            this.mongo = mongo;
        }

        public IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit)
        {
            return Collections.FindAs<Customer>(Query.EQ("OwnerId",BsonValue.Create(ownerId))).
                SetSkip(start).SetLimit(limit);
        }

        public int CountCustomers(string ownerId)
        {
            return Convert.ToInt32(Collections.Count(Query.EQ("OwnerId", BsonValue.Create(ownerId))));
        }

        public Customer GetByCustName(string ownerId, string custName)
        {
            return Collections.FindOneAs<Customer>(Query.And(
                Query.EQ("OwnerId", BsonValue.Create(ownerId)),
                Query.EQ("Name", BsonValue.Create(custName))));
        }

        private MongoCollection<Customer> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Customer>("customers"); }
        }
    }
}