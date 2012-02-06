using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace dokuku.sales.invoices.readmodel
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public MongoConfig Mongo { get; set; }

        public IEnumerable<Invoice> AllInvoices(string OwnerId)
        {
            return Collection.FindAs<Invoice>(Query.EQ("OwnerId", BsonValue.Create(OwnerId)));
        }
        public IEnumerable<Invoice> GetDataInvoiceToPaging(string ownerId, int start, int limit)
        {
            return Collection.FindAs<Invoice>(Query.EQ("OwnerId", BsonValue.Create(ownerId))).SetSkip(start)
                .SetLimit(limit).OrderByDescending<Invoice, DateTime>(x => x.InvoiceDate).ToArray();
        }
        public int CountInvoice(string OwnerId)
        {
            return Convert.ToInt32(Collection.Count(Query.EQ("OwnerId", BsonValue.Create(OwnerId))));
        }

        public Invoice FindById(Guid id, string ownerId)
        {
            return Collection.FindOneAs<Invoice>(Query.And(
                                Query.EQ("_id", id),
                                Query.EQ("OwnerId", ownerId)
                ));
        }

        public IEnumerable<InvoiceIndex> Search(string ownerId, string[] keywords)
        {
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            return IndexCollection.FindAs<InvoiceIndex>(qry).SetLimit(10);
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

        public Invoice FindById(Guid guid)
        {
            return Collection.FindOneAs<Invoice>(Query.And(
                                Query.EQ("_id", guid)));
        }

        private MongoCollection Collection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(invoiceresource.InvoiceReportCollectionName);
            }
        }
        private MongoCollection IndexCollection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(invoiceresource.InvoiceIndexCollectionName);
            }
        }
    }
}
