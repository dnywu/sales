using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.paymentterms.model;

namespace dokuku.sales.paymentterms.query
{
    public class PaymentTermsQuery : IPaymentTermsQuery
    {
        MongoCollection<BsonDocument> _collections;
        public PaymentTermsQuery(MongoConfig config)
        {
            _collections = config.MongoDatabase.GetCollection(typeof(PaymentTerms).Name);
        }
        public PaymentTerms Get(Guid id,string ownerId)
        {
            return _collections.FindOneAs<PaymentTerms>(Query.EQ("_id", id));
        }
        public PaymentTerms FindByName(string name,string ownerId)
        {
            return _collections.FindOneAs<PaymentTerms>(Query.And(Query.EQ("Name", name), Query.EQ("OwnerId", ownerId)));
        }
        public PaymentTerms FindByNameAndId(string name, Guid id,string ownerId)
        {
            return _collections.FindOneAs<PaymentTerms>(Query.And(Query.EQ("Name", name), Query.EQ("_id", id), Query.EQ("OwnerId", ownerId)));
        }
        public PaymentTerms[] FindAll(string ownerId)
        {
            return _collections.FindAs<PaymentTerms>(Query.EQ("OwnerId", ownerId)).ToArray();
        }
    }
}