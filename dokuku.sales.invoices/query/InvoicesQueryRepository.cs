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
        public IEnumerable<Invoices> AllInvoices()
        {
            return Collections.FindAllAs<Invoices>();
        }
        private MongoCollection<Invoices> Collections
        {
            get
            {
                return mongo.MongoDatabase.GetCollection<Invoices>("invoices");
            }
        }
        public IEnumerable<Invoices> Search(string ownerId, string[] keywords)
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
