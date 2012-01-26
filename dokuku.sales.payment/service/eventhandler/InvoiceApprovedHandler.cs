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
            BsonBinaryData invoiceId = (BsonBinaryData)invoice["_id"];
            InvoicePayment paymentInvoice = new InvoicePayment(
                (Guid)invoiceId.RawValue,
                invoice["OwnerId"].ToString(),
                new Invoice((Guid)invoiceId.RawValue,
                    invoice["InvoiceNo"].ToString(),
                    Convert.ToDecimal(invoice["Total"]),(DateTime)invoice["InvoiceDate"]));
            Mongo.MongoDatabase.GetCollection<InvoicePayment>(typeof(InvoicePayment).Name).Save<InvoicePayment>(paymentInvoice);
        }
    }
}