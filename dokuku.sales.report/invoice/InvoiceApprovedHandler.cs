//using dokuku.sales.config;
//using dokuku.sales.invoice.messages;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using MongoDB.Driver.Builders;
//using NServiceBus;

//namespace dokuku.sales.report.Handlers
//{
//    public class InvoiceApprovedHandler : IMessageHandler<InvoiceApproved>
//    {
//        public MongoConfig Config { get; set; }
//        public void Handle(InvoiceApproved message)
//        {
//            BsonDocument doc = BsonDocument.Parse(message.InvoiceJson);
//            BsonDocument index = new BsonDocument();

//            index["Keywords"] = BsonValue.Create(new string[]{
//                    doc["_id"].ToString(),
//                    doc["Customer"].ToString(),
//                    doc["CustomerId"].ToString(),
//                    doc["InvoiceDate"].ToString(),
//                    doc["DueDate"].ToString(),
//                    doc["LateFee"].ToString(),
//                    doc["Note"].ToString(),
//                    doc["PONo"].ToString(),
//                    doc["Status"].ToString(),
//                    doc["SubTotal"].ToString(),
//                    doc["TermCondition"].ToString(),
//                    doc["Total"].ToString(),
//                    doc["OwnerId"].ToString()});

//            index["InvoiceNo"] = doc["InvoiceNo"];
//            index["Customer"] = doc["Customer"];
//            index["OwnerId"] = doc["OwnerId"];
//            index["_id"] = doc["_id"];
//            index["PONo"] = doc["PONo"];
//            collection.Save(index);
//        }

//        private MongoCollection collection
//        {
//            get { return Config.MongoDatabase.GetCollection(CollectionName.INVOICE_REPORTS); }
//        }
//    }
//}