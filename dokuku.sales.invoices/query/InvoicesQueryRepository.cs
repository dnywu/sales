using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Text.RegularExpressions;
namespace dokuku.sales.invoices.query
{
    public class InvoicesQueryRepository : IInvoicesQueryRepository
    {
        MongoConfig mongo;
        public InvoicesQueryRepository(MongoConfig mongo)
        {
            this.mongo = mongo;
        }
        public IEnumerable<Invoices> AllInvoices(string OwnerId)
        {
            return Collections.FindAs<Invoices>(Query.EQ("OwnerId",BsonValue.Create(OwnerId)));
        }
        private MongoCollection<Invoices> Collections
        {
            get
            {
                return mongo.MongoDatabase.GetCollection<Invoices>("invoices");
            }
        }
        public IEnumerable<InvoiceReports> Search(string ownerId, string[] keywords)
        {
            MongoCollection<InvoiceReports> reportCollection = mongo.ReportingDatabase.GetCollection<InvoiceReports>(typeof(InvoiceReports).Name);
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            return reportCollection.Find(qry).SetLimit(10);
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
