using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using dokuku.sales.customer.model;
namespace dokuku.sales.customer.repository
{
    public class CustomerRepository : ICustomerRepository
    {
        MongoConfig mongo;
        public CustomerRepository(MongoConfig mongoconfig)
        {
            mongo = mongoconfig;
        }

        public void Save(Customer cs)
        {
            collection.Save<Customer>(cs);
        }

        public void Delete(Guid id)
        {
            QueryDocument qryDoc = new QueryDocument();
            qryDoc["_id"] = id;
            collection.Remove(qryDoc);
        }
        public Customer Get(Guid id, string ownerId)
        {
            QueryDocument qry = new QueryDocument() {
                                    {"_id",id},
                                    {"OwnerId",ownerId}};
            return collection.Find(qry).FirstOrDefault();
        }
        private MongoCollection<Customer> collection
        {
            get { return db.GetCollection<Customer>("customers"); }
        }
        private MongoDatabase db
        {
            get { return mongo.MongoDatabase; }
        }
    }
}