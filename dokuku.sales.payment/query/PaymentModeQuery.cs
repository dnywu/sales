using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.payment.domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Text.RegularExpressions;

namespace dokuku.sales.payment.query
{
    public class PaymentModeQuery : IPaymentModeQuery
    {
        MongoCollection<BsonDocument> _collections;
        public PaymentModeQuery(MongoConfig config)
        {
            _collections = config.MongoDatabase.GetCollection(typeof(PaymentMode).Name);
        }
        public PaymentMode Get(Guid id)
        {
            return _collections.FindOneAs<PaymentMode>(Query.EQ("_id", id));
        }
        public PaymentMode FindByName(string name, string ownerId)
        {
            return _collections.FindOneAs<PaymentMode>(Query.And(Query.EQ("OwnerId", ownerId), Query.EQ("Name", new Regex(name, RegexOptions.IgnoreCase))));
        }
        public PaymentMode FindByNameAndId(string name, Guid id, string ownerId)
        {
            return _collections.FindOneAs<PaymentMode>(Query.And(Query.EQ("Name", new Regex(name, RegexOptions.IgnoreCase)), Query.EQ("_id", id), Query.EQ("OwnerId", ownerId)));
        }
        public IEnumerable<PaymentMode> FindAll(string ownerId)
        {
            return _collections.FindAs<PaymentMode>(Query.EQ("OwnerId", ownerId));
        }
    }
}