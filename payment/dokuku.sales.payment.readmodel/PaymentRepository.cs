using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Driver;
namespace dokuku.sales.payment.readmodel
{
    public class PaymentRepository : IPaymentRepository
    {
        public MongoConfig Mongo { get; set; }
        
        public IEnumerable<Payment> FindAll()
        {
            return Collection.FindAllAs<Payment>();
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