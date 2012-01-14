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
        MongoDatabase reportingDB;
        MongoCollection<Customer> reportCollections;
        public CustomerReportRepository()
        {
            reportingDB = MongoConfig.Instance.ReportingDatabase;
            reportCollections = reportingDB.GetCollection<Customer>("customers");
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
    }
}
