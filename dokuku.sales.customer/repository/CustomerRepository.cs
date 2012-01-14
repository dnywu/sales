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
        MongoDatabase db;
        MongoCollection<Customer> collection;
        public CustomerRepository()
        {
            db = MongoConfig.Instance.MongoDatabase;
            collection = db.GetCollection<Customer>("customers");
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
    }
}