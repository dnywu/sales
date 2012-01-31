using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.payment.readmodel
{
    public class PaymentRepository : IPaymentRepository
    {
        public MongoConfig Mongo { get; set; }
        
        public IEnumerable<Payment> FindAll(string ownerId)
        {
            return Collection.FindAs<Payment>(Query.EQ("OwnerId", ownerId));
        }

        private MongoCollection Collection
        {
            get
            {
                return Mongo.ReportingDatabase.GetCollection(paymentresource.PaymentReportCollectionName);
            }
        }
    }
}