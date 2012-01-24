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

        public void Save(Invoices invoice)
        {
            Collections.Save<Invoices>(invoice);
        }

        public void UpdateInvoices(Invoices invoice)
        {
            Collections.Save<Invoices>(invoice);
        }

        public Invoices Get(Guid id, string ownerId)
        {
            var qry = Query.And(
                Query.EQ("_id", id),
                Query.EQ("OwnerId", ownerId));
            
            return Collections.FindOneAs<Invoices>(qry);
        }

        public void Delete(Guid id, string ownerId)
        {
            Collections.Remove(Query.And(
                Query.EQ("_id", id),
                Query.EQ("OwnerId", ownerId)));
        }
        
        private MongoCollection<Invoices> Collections
        {
            get
            {
                return mongo.MongoDatabase.GetCollection<Invoices>(typeof(Invoices).Name);
            }
        }

        public Invoices GetInvByNumber(string invoiceNumber, string ownerId)
        {
            var qry = Query.And(
                Query.EQ("InvoiceNo", invoiceNumber),
                Query.EQ("OwnerId", ownerId));

            return Collections.FindOneAs<Invoices>(qry);
        }
    }
}