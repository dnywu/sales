using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using dokuku.sales.customer.model;
using dokuku.sales.config;

namespace dokuku.sales.customer.repository
{
    public class CustomerReportRepository : ICustomerReportRepository
    {
        MongoConfig mongo;
        public CustomerReportRepository(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }

        public IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit)
        {
            QueryDocument qry = new QueryDocument();
            qry["OwnerId"] = ownerId;
            MongoCursor<Customer> docs = reportCollections.Find(qry).SetSkip(start).SetLimit(limit);
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
            MongoCursor<Customer> docs = reportCollections.Find(qry);
            return Int32.Parse(docs.Count().ToString());
        }

        public Customer GetByCustName(string ownerId, string custName)
        {
            QueryDocument qry = new QueryDocument() {
                                    {"OwnerId",ownerId},
                                    {"Name",custName}};
            return reportCollections.FindOneAs<Customer>(qry);
        }
        private MongoDatabase reportDatabase
        {
            get { return mongo.ReportingDatabase; }
        }
        private MongoCollection<Customer> reportCollections
        {
            get { return reportDatabase.GetCollection<Customer>("customers"); }
        }
    }
}
