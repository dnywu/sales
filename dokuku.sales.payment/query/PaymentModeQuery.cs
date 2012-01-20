using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.payment
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
    }
}