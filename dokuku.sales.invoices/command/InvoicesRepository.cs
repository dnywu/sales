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
            SaveAsReport(invoice);
        }

        public Invoices Get(string id, string ownerId)
        {
            var qry = Query.And(
                Query.EQ("_id", id),
                Query.EQ("OwnerId", ownerId));
            
            return Collections.FindOneAs<Invoices>(qry);
        }

        public void Delete(string id, string ownerId)
        {
            Collections.Remove(Query.And(
                Query.EQ("_id", id),
                Query.EQ("OwnerId", ownerId)));
            RemoveReport(id, ownerId);
        }
        
        private MongoCollection<Invoices> Collections
        {
            get
            {
                return mongo.MongoDatabase.GetCollection<Invoices>("invoices");
            }
        }

        private void SaveAsReport(Invoices inv)
        {
            MongoCollection<InvoiceReports> collections = mongo.MongoDatabase.GetCollection<InvoiceReports>(typeof(InvoiceReports).Name);
            InvoiceReports invReport = new InvoiceReports
            {
                _id = inv._id,
                OwnerId = inv.OwnerId,
                CustomerName = inv.Customer,
                PONumber = inv.PONo,
                Keywords = new string[]{
                    inv._id,
                    inv.Customer,
                    inv.CustomerId,
                    inv.DueDate.AsString(),
                    inv.InvoiceDate.AsString(),
                    inv.LateFee,
                    inv.Note,
                    inv.PONo,
                    inv.Status,
                    inv.SubTotal.ToString(),
                    inv.TermCondition,
                    inv.Terms.ToString(),
                    inv.Total.ToString()
                }
            };
            collections.Insert<InvoiceReports>(invReport);
            collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private void RemoveReport(string id, string ownerId)
        {
            MongoCollection<InvoiceReports> reportCollections = mongo.MongoDatabase.GetCollection<InvoiceReports>(typeof(InvoiceReports).Name);
            reportCollections.Remove(Query.And(
                Query.EQ("_id", id),
                Query.EQ("OwnerId", ownerId)));
        }
    }
}