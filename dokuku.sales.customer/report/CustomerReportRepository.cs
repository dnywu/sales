using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using dokuku.sales.customer.model;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Text.RegularExpressions;
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
                Query.EQ("Name", new Regex("^" + custName + "$", RegexOptions.IgnoreCase))));
        }

        public IEnumerable<CustomerReports> Search(string ownerId, string[] keywords)
        {
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            return searchndexCollections.Find(qry).SetLimit(10);
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

        public Customer GetCustomerById(Guid id)
        {
            QueryDocument qry = new QueryDocument() { {"_id",id}};
            return Collections.FindOneAs<Customer>(qry);
        }
        private MongoCollection<Customer> Collections
        {
            get { return mongo.ReportingDatabase.GetCollection<Customer>(typeof(Customer).Name); }
        }
        private MongoCollection<CustomerReports> searchndexCollections
        {
            get
            {
                return mongo.ReportingDatabase.GetCollection<CustomerReports>(typeof(CustomerReports).Name);
            }
        }

        public IEnumerable<Customer> GetCustomerByOwnerIdandGuid(string ownerid, Guid[] ids)
        {
            BsonValue[] values = new BsonValue[ids.Length];
            int i = 0;
            foreach(Guid id in ids)
            {
                values[i] = BsonValue.Create(id);
                i++;
            }
            var qry = Query.And(Query.EQ("OwnerId", ownerid),
                      Query.In("_id", values));
            return Collections.Find(qry);
        }
    }
}
