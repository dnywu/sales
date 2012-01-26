using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using dokuku.sales.paymentmode.model;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.paymentmode.service
{
    public class PaymentModeService : IPaymentModeService
    {
        MongoCollection<BsonDocument> collections;
        public PaymentModeService(MongoConfig mongo)
        {
            collections = mongo.MongoDatabase.GetCollection(typeof(PaymentModes).Name);
        }
        public PaymentModes Insert(string json, string ownerId)
        {
            PaymentModes paymentMode = JsonConvert.DeserializeObject<PaymentModes>(json);
            paymentMode._id = Guid.NewGuid();
            paymentMode.OwnerId = ownerId;
            collections.Save(paymentMode);
            return paymentMode;
        }
        public PaymentModes Update(string json,string ownerId)
        {
            PaymentModes paymentMode = JsonConvert.DeserializeObject<PaymentModes>(json);
            paymentMode.OwnerId = ownerId;
            collections.Save(paymentMode);
            return paymentMode;
        }
        public void Delete(Guid id)
        {
            collections.Remove(Query.EQ("_id", id));
        }
    }
}