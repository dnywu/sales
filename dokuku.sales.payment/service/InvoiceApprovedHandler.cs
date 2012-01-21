using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.invoice.messages;
using dokuku.sales.config;
using MongoDB.Bson;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.service
{
    public class InvoiceApprovedHandler : IHandleMessages<InvoiceApproved>
    {
        public MongoConfig Mongo { get; set; }
        public void Handle(InvoiceApproved message)
        {
            BsonDocument invoice = BsonDocument.Parse(message.InvoiceJson);
            InvoicePayment paymentInvoice = new InvoicePayment(
                Guid.NewGuid(),
                invoice["OwnerId"].ToString(),
                new Invoice(Guid.Parse(invoice["_id"].ToString()),
                    invoice["InvoiceNo"].ToString(),
                    Convert.ToDecimal(invoice["Total"])),
                Guid.Parse(invoice["CustomerId"].ToString()));
            Mongo.MongoDatabase.GetCollection(typeof(InvoicePayment).Name).Save(paymentInvoice);
        }
    }
}