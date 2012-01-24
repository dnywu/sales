using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.payment.domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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
        public PaymentMode FindByName(string name)
        {
            return _collections.FindOneAs<PaymentMode>(Query.EQ("Name", name));
        }
        public PaymentMode FindByNameAndId(string name, Guid id)
        {
            return _collections.FindOneAs<PaymentMode>(Query.And(Query.EQ("Name", name), Query.EQ("_id", id)));
        }
        public IEnumerable<PaymentMode> FindAll()
        {
            return _collections.FindAllAs<PaymentMode>();
        }
    }
}