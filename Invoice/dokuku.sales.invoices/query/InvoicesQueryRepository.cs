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
        public IEnumerable<Invoices> GetDataInvoiceToPaging(string ownerId, int start, int limit)
        {
            return Collections.FindAs<Invoices>(Query.EQ("OwnerId", BsonValue.Create(ownerId))).SetSkip(start)
                .SetLimit(limit).OrderByDescending<Invoices,DateTime>(x => x.InvoiceDate).ToArray();
        }
        public int CountInvoice(string OwnerId) 
        {
           return Convert.ToInt32(Collections.Count(Query.EQ("OwnerId",BsonValue.Create(OwnerId))));
        }
        
        public Invoices FindById(Guid id, string ownerId)
        {
            return Collections.FindOneAs<Invoices>(Query.And(
                                Query.EQ("_id",id),
                                Query.EQ("OwnerId",ownerId)
                ));
        }

        public IEnumerable<InvoiceReports> Search(string ownerId, string[] keywords)
        {
            MongoCollection<InvoiceReports> reportCollection = mongo.ReportingDatabase.GetCollection<InvoiceReports>(typeof(InvoiceReports).Name);
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            InvoiceReports invReport = reportCollection.Find(qry).FirstOrDefault();
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
        private MongoCollection<Invoices> Collections
        {
            get
            {
                return mongo.ReportingDatabase.GetCollection<Invoices>(typeof(Invoices).Name);
            }
        }


        public Invoices FindById(Guid guid)
        {
            return Collections.FindOneAs<Invoices>(Query.And(
                                Query.EQ("_id", guid)));
        }
    }
}
