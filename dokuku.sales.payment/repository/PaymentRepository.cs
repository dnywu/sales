using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.domain;
using MongoDB.Driver;
using dokuku.sales.config;
namespace dokuku.sales.payment.repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public MongoConfig Mongo {get;set;}
        public void Save(InvoicePayment invoicePayment)
        {
            Collections.Save(invoicePayment);
        }

        public void Update(InvoicePayment invoicePayment)
        {
            Collections.Save(invoicePayment);
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(typeof(InvoicePayment).Name); }
        }
    }
}
