using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.messages;
using NServiceBus;
using dokuku.sales.config;
using dokuku.sales.payment.domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.payment.service
{
    public class RevisePaymentHandler : IHandleMessages<RevisePayment>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(RevisePayment message)
        {
            InvoicePayment invPayment = Collections.FindOneAs<InvoicePayment>(Query.EQ("_id", message.InvoiceId));
            invPayment.RevisePayment(message.AdjustedPaymentId,PaymentRecord.
                            AmountPaid(message.AmountPaid).
                            BankCharge(message.BankCharge).
                            PaymentDate(message.PaymentDate).
                            PaymentMode(new PaymentMode() { _id = message.PaymentModeId }).
                            Reference(message.Reference).
                            Notes(message.Notes));
            Collections.Save(invPayment);
        }
        private MongoCollection Collections
        {
            get { return Mongo.MongoDatabase.GetCollection(typeof(InvoicePayment).Name); }
        }
    }
}
