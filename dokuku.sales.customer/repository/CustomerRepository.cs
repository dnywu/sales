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
            CollectionReports.Save<CustomerReports>(new CustomerReports(customer));
            CollectionReports.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }

        public void UpdateCustomer(Customer item)
        {
            var qry = Query.EQ("_id", item._id);
            var update = Update.Replace<Customer>(item);
            Collections.Update(qry, update);
            updateIndex(item);
        }

        public void Delete(Guid id)
        {
            Collections.Remove(Query.EQ("_id", BsonValue.Create(id)));
            deleteIndex(id);
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

        private MongoDatabase DB
        {
            get { return mongo.MongoDatabase; }
        }
        private MongoCollection<CustomerReports> CollectionReports
        {
            get
            {
                return DB.GetCollection<CustomerReports>(typeof(CustomerReports).Name);
            }
        }
        private void deleteIndex(Guid id)
        {
            CollectionReports.Remove(Query.EQ("_id", id));
        }
        private void updateIndex(Customer cust)
        {
            var qry = Query.EQ("_id", cust._id);
            var update = Update.Replace<Customer>(cust);
            CollectionReports.Update(qry, update);
        }
    }
}