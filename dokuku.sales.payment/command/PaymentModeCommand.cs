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
    public class PaymentModeCommand : IPaymentModeCommand
    {
        MongoCollection<BsonDocument> _collections;
        public PaymentModeCommand(MongoConfig config)
        {
            _collections = config.MongoDatabase.GetCollection(typeof(PaymentMode).Name);
        }
        public void Save(PaymentMode paymentMode)
        {
            _collections.Insert(paymentMode.ToBsonDocument());
        }
        public void Update(PaymentMode paymentMode)
        {
            _collections.Save(paymentMode);
        }
        public void Delete(Guid id)
        {
            _collections.Remove(Query.EQ("_id", id));
        }
    }
}