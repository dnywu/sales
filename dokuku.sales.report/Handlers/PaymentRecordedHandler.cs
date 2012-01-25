//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NServiceBus;
//using dokuku.sales.payment.messages;
//using dokuku.sales.config;
//using MongoDB.Bson;
//namespace dokuku.sales.report.Handlers
//{
//    public class PaymentRecordedHandler : IHandleMessages<PaymentIsRecorded>
//    {
//        public MongoConfig Mongo { get; set; }
//        public void Handle(PaymentIsRecorded message)
//        {
//            BsonDocument doc = BsonDocument.Parse(message.PaymentJson);
//            BsonDocument index = new BsonDocument();

//            index["Keywords"] = BsonValue.Create(new string[13]{
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
//            Collections.Save(index);
//            Collections.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
//        }
//    }
//}