using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using dokuku.sales.paymentterms.model;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.paymentterms.service
{
    public class PaymentTermsService : IPaymentTermsService
    {
        MongoCollection<BsonDocument> collections;
        public PaymentTermsService(MongoConfig mongo)
        {
            collections = mongo.MongoDatabase.GetCollection(typeof(PaymentTerms).Name);
        }
        public PaymentTerms Insert(string json, string ownerId)
        {
            PaymentTerms paymentTerms = JsonConvert.DeserializeObject<PaymentTerms>(json);
            paymentTerms._id = Guid.NewGuid();
            paymentTerms.OwnerId = ownerId;
            collections.Save(paymentTerms);
            return paymentTerms;
        }
        public PaymentTerms Update(string json,string ownerId)
        {
            PaymentTerms paymentTerms = JsonConvert.DeserializeObject<PaymentTerms>(json);
            paymentTerms.OwnerId = ownerId;
            collections.Save(paymentTerms);
            return paymentTerms;
        }
        public void Delete(Guid id)
        {
            collections.Remove(Query.EQ("_id", id));
        }
    }
}