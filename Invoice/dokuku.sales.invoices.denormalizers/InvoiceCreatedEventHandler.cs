//using System;
//using NServiceBus;
//using Ncqrs.NServiceBus;
//using dokuku.sales.invoices.events;
//using MongoDB.Driver;
//using dokuku.sales.config;
//using MongoDB.Bson;
//using MongoDB.Driver.Builders;
//using dokuku.sales.invoices.common;

//namespace dokuku.sales.invoices.denormalizers
//{
//    class InvoiceCreatedEventHandler : IMessageHandler<EventMessage<InvoiceCreated>>
//    {
//        public MongoConfig Mongo { get; set; }
//        public void Handle(EventMessage<InvoiceCreated> message)
//        {
//            Customer customer = CustomerCollection.FindOneAs<Customer>(Query.EQ("_id", BsonValue.Create(message.Payload.CustomerId)));
//            saveInvoice(message,customer);
//            //CreateIndex(message);
//        }

//        private void saveInvoice(EventMessage<InvoiceCreated> message,Customer customer)
//        {
//            BsonDocument doc = message.Payload.ToBsonDocument();
//            doc["_id"] = BsonValue.Create(message.Payload.InvoiceId);
//            doc["InvoiceDate"] = BsonValue.Create(DateTime.Now.Date);
//            doc["Customer"] = BsonValue.Create(customer);
//            doc["DueDate"] = BsonValue.Create(message.Payload.DueDate);
//            doc["Term"] = BsonValue.Create(customer.Term);
//            doc["Currency"] = BsonValue.Create(message.Payload.Currency);
//            doc["BaseCurrency"] = BsonValue.Create(message.Payload.BaseCurrency);
//            doc["Rate"] = BsonValue.Create(message.Payload.Rate);
//            doc["UserName"] = BsonValue.Create(message.Payload.UserName);
//            doc["Status"] = BsonValue.Create(message.Payload.Status);
//            Collection.Save(doc);
//        }

//        private void CreateIndex(EventMessage<InvoiceCreated> message,Customer customer)
//        {
//            //BsonDocument doc = message.Payload.ToBsonDocument();
//            BsonDocument index = new BsonDocument();

//            index["Keywords"] = BsonValue.Create(new string[]{
//                    message.Payload.InvoiceId == null ? "" : message.Payload.InvoiceId.ToString(),
//                    message.Payload.InvoiceNo == null ? "" : message.Payload.InvoiceNo,
//                    message.Payload.PONo == null ? "" : message.Payload.PONo,
//                    customer.Name,
//                    message.Payload.UserName == null ? "" : message.Payload.UserName,
//                    message.Payload.OwnerId == null ? "" : message.Payload.OwnerId,
//                    message.Payload.UserName});

//            index["InvoiceNo"] = BsonValue.Create(message.Payload.InvoiceNo == null ? "" : message.Payload.InvoiceNo);
//            index["Customer"] = BsonValue.Create(customer.Name);
//            index["OwnerId"] = BsonValue.Create(message.Payload.OwnerId == null ? "" : message.Payload.OwnerId);
//            index["_id"] = BsonValue.Create(message.Payload.InvoiceId == null ? "" : message.Payload.InvoiceId.ToString());
//            index["PONo"] = BsonValue.Create(message.Payload.PONo == null ? "" : message.Payload.PONo);
//            index["UserName"] = BsonValue.Create(message.Payload.UserName);
//            IndexCollection.Save(index);
//            IndexCollection.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
//        }
//        private MongoCollection Collection
//        {
//            get
//            {
//                return Mongo.ReportingDatabase.GetCollection(invoiceresources.InvoiceReportCollectionName);
//            }
//        }
//        private MongoCollection IndexCollection
//        {
//            get
//            {
//                return Mongo.ReportingDatabase.GetCollection(invoiceresources.InvoiceIndexCollectionName);
//            }
//        }
//        private MongoCollection CustomerCollection
//        {
//            get
//            {
//                return Mongo.ReportingDatabase.GetCollection(invoiceresources.InvoiceCustomerCollectionName);
//            }
//        }
//    }
//}
