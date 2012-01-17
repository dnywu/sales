using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using dokuku.sales.customer.model;
using MongoDB.Driver.Builders;
namespace dokuku.sales.customer.repository
{
    public class CustomerRepository : ICustomerRepository
    {
        MongoConfig mongo;
        public CustomerRepository(MongoConfig mongo)
        {
            this.mongo = mongo;
        }

        public void Save(Customer cs)
        {
            Collections.Save<Customer>(cs);
        }

        public void Delete(Guid id)
        {
            Collections.Remove(Query.EQ("_id", BsonValue.Create(id)));
        }
        public Customer Get(Guid id, string ownerId)
        {
            return Collections.FindOneAs<Customer>(Query.And(
                Query.EQ("_id", BsonValue.Create(id)), 
                Query.EQ("OwnerId", BsonValue.Create(ownerId))));
        }
        private MongoCollection<Customer> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Customer>("customers"); }
        }
    }
}