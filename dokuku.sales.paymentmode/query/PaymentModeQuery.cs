using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.paymentmode.model;

namespace dokuku.sales.paymentmode.query
{
    public class PaymentModeQuery : IPaymentModeQuery
    {
        MongoCollection<BsonDocument> _collections;
        public PaymentModeQuery(MongoConfig config)
        {
            _collections = config.MongoDatabase.GetCollection(typeof(PaymentModes).Name);
        }
        public PaymentModes Get(Guid id,string ownerId)
        {
            return _collections.FindOneAs<PaymentModes>(Query.EQ("_id", id));
        }
        public PaymentModes FindByName(string name,string ownerId)
        {
            return _collections.FindOneAs<PaymentModes>(Query.And(Query.EQ("Name", name), Query.EQ("OwnerId", ownerId)));
        }
        public PaymentModes FindByNameAndId(string name, Guid id,string ownerId)
        {
            return _collections.FindOneAs<PaymentModes>(Query.And(Query.EQ("Name", name), Query.EQ("_id", id), Query.EQ("OwnerId", ownerId)));
        }
        public PaymentModes[] FindAll(string ownerId)
        {
            return _collections.FindAs<PaymentModes>(Query.EQ("OwnerId", ownerId)).ToArray();
        }
    }
}