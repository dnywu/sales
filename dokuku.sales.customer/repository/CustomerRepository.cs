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
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }

        public void Delete(Guid id)
        {
            QueryDocument qryDoc = new QueryDocument();
            qryDoc["_id"] = id;
            Collections.Remove(qryDoc);
        }
        public Customer Get(Guid id, string ownerId)
        {
            QueryDocument qry = new QueryDocument() {
                                    {"_id",id},
                                    {"OwnerId",ownerId}};
            return Collections.Find(qry).FirstOrDefault();
        }
        private MongoCollection<Customer> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Customer>("customers"); }
        }
    }
}