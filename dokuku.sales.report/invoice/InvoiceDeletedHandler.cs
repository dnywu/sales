//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using dokuku.sales.invoice.messages;
//using NServiceBus;
//using dokuku.sales.config;
//using MongoDB.Driver;
//using MongoDB.Driver.Builders;
//namespace dokuku.sales.report.Handlers
//{
//    public class InvoiceDeletedHandler : IHandleMessages<InvoiceDeleted>
//    {
//        public MongoConfig Mongo { get; set; }
//        public void Handle(InvoiceDeleted message)
//        {
//            Collections.Remove(Query.And(
//               Query.EQ("_id", message.Id),
//               Query.EQ("OwnerId", message.OwnerId)));
//        }

//        private MongoCollection Collections
//        {
//            get { return Mongo.MongoDatabase.GetCollection(CollectionName.INVOICE_REPORTS); }
//        }
//    }
//}