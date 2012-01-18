using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

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
    }
}
