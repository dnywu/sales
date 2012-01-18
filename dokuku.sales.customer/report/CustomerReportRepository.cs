using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using dokuku.sales.customer.model;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using System.Text.RegularExpressions;
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
            QueryDocument qry = new QueryDocument();
            qry["OwnerId"] = ownerId;
            MongoCursor<Customer> docs = Collections.Find(qry).SetSkip(start).SetLimit(limit);
            IList<Customer> customers = new List<Customer>();
            foreach (Customer cust in docs)
            {
                customers.Add(cust);
            }
            return customers.ToArray<Customer>();
        }

        public int CountCustomers(string ownerId)
        {
            QueryDocument qry = new QueryDocument();
            qry["OwnerId"] = ownerId;
            MongoCursor<Customer> docs = Collections.Find(qry);
            return Int32.Parse(docs.Count().ToString());
        }

        public Customer GetByCustName(string ownerId, string custName)
        {
            QueryDocument qry = new QueryDocument() {
                                    {"OwnerId",ownerId},
                                    {"Name",custName}};
            return Collections.FindOneAs<Customer>(qry);
        }

        private MongoCollection<Customer> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Customer>("customers"); }
        }
        public IEnumerable<Customer> Search(string ownerId, string[] keywords)
        {
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            return Collections.Find(qry);
        }
        private QueryComplete getQuery(string[] keywords)
        {
            QueryComplete[] qries = new QueryComplete[keywords.Length];
            int index = 0;
            foreach (string keyword in keywords)
            {
                qries[index] = Query.EQ("Keywords", new Regex(keyword, RegexOptions.IgnoreCase));
                index++;
            }
            return Query.Or(qries);
        }
    }
}