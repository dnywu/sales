using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.payment.domain;
using MongoDB.Driver;
namespace dokuku.sales.payment
{
    public class PaymentRepository : IPaymentRepository
    {
        public MongoConfig Mongo { get; set; }

        public PaymentRepository()
        {
        }

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
            get { return Mongo.MongoDatabase.GetCollection<InvoicePayment>(typeof(InvoicePayment).Name); }
        }
    }
}