using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using System.Configuration;
using MongoDB.Driver;
using dokuku.sales.invoices.model;
using MongoDB.Driver.Builders;

namespace dokuku.sales.invoices.command
{
    public class InvoicesRepository : IInvoicesRepository
    {
        MongoConfig mongo;
        public InvoicesRepository(MongoConfig mongo)
        {
            this.mongo = mongo;
        }

        public void Save(Invoices ci)
        {
            Collections.Save<Invoices>(ci);
            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }

        public Invoices Get(string id)
        {
            var qry = Query.EQ("_id", id);
            return Collections.FindOneAs<Invoices>(qry);
        }

        public void Delete(string id)
        {
            Collections.Remove(Query.EQ("_id", id));
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
