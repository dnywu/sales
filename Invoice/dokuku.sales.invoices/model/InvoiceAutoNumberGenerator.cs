using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.invoices.model;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson;
namespace dokuku.sales.invoices.model
{
    public class InvoiceAutoNumberGenerator : IInvoiceAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "INV-";
        public const string COMPANY_ID_FIELD = "CompanyId";
        MongoConfig mongo;
        DateTime transactionDate;
        string companyId;
        public InvoiceAutoNumberGenerator(MongoConfig mongoConfig)
        {
            this.mongo = mongoConfig;
        }
        public string GenerateInvoiceNumberDraft(string companyId)
        {
            this.companyId = companyId;
            return mongo.Save(GetInvoiceAutoNumberDraft().Next()).InvoiceNumberInStringFormat();
        }
        public string GenerateInvoiceNumber(DateTime transactionDate, string companyId)
        {
            this.transactionDate = transactionDate;
            this.companyId = companyId;
            InvoiceAutoNumberConfig cfg = GetInvoiceAutoNumberConfig(companyId);

            switch (cfg.Mode)
            {
                case AutoNumberMode.Yearly:
                    return mongo.Save(GetInvoiceAutoNumberYearly().Next()).InvoiceNumberInStringFormat(cfg.Prefix);
                case AutoNumberMode.Monthly:
                    return mongo.Save(GetInvoiceAutoNumberMonthly().Next()).InvoiceNumberInStringFormat(cfg.Prefix);
                default:
                    return mongo.Save(GetInvoiceAutoNumberDefault().Next()).InvoiceNumberInStringFormat(cfg.Prefix);
            }
        }

        private InvoiceAutoNumberDraft GetInvoiceAutoNumberDraft()
        {
            MongoCollection<InvoiceAutoNumberDraft> collection = mongo.MongoDatabase.GetCollection<InvoiceAutoNumberDraft>(typeof(InvoiceAutoNumberDraft).Name);
            InvoiceAutoNumberDraft invoiceAutoNumber = collection.FindOneAs<InvoiceAutoNumberDraft>(Query.And(
                Query.EQ("_id", BsonValue.Create(typeof(InvoiceAutoNumberDraft).Name + "-"+ companyId)),
                Query.EQ(COMPANY_ID_FIELD, BsonValue.Create(companyId))));

            if (invoiceAutoNumber == null)
            {
                invoiceAutoNumber = new InvoiceAutoNumberDraft(typeof(InvoiceAutoNumberDraft).Name, companyId);
                collection.Save<InvoiceAutoNumberDraft>(invoiceAutoNumber);
            }

            return invoiceAutoNumber;
        }
        public InvoiceAutoNumberConfig GetInvoiceAutoNumberConfig(string companyId)
        {
            MongoCollection<InvoiceAutoNumberConfig> collection =  mongo.MongoDatabase.GetCollection<InvoiceAutoNumberConfig>(typeof(InvoiceAutoNumberConfig).Name);
            InvoiceAutoNumberConfig cfg = collection.FindOneAs<InvoiceAutoNumberConfig>(Query.And(
                Query.EQ("_id", typeof(InvoiceAutoNumberConfig).Name),
                Query.EQ(COMPANY_ID_FIELD, BsonValue.Create(companyId))));
            
            if (cfg == null)
            {
                cfg = new InvoiceAutoNumberConfig(typeof(InvoiceAutoNumberConfig).Name, AutoNumberMode.Default, DEFAULT_PREFIX, companyId);
                collection.Save<InvoiceAutoNumberConfig>(cfg);
            }

            return cfg;
        }
        private InvoiceAutoNumberDefault GetInvoiceAutoNumberDefault()
        {
            MongoCollection<InvoiceAutoNumberDefault> collection = mongo.MongoDatabase.GetCollection<InvoiceAutoNumberDefault>(typeof(InvoiceAutoNumberDefault).Name);
            InvoiceAutoNumberDefault invoiceAutoNumber = collection.FindOneAs<InvoiceAutoNumberDefault>(Query.And(
                Query.EQ("_id", BsonValue.Create(typeof(InvoiceAutoNumberDefault).Name)),
                Query.EQ(COMPANY_ID_FIELD, BsonValue.Create(companyId))));
            
            if (invoiceAutoNumber == null)
            {
                invoiceAutoNumber = new InvoiceAutoNumberDefault(typeof(InvoiceAutoNumberDefault).Name, companyId);
                collection.Save<InvoiceAutoNumberDefault>(invoiceAutoNumber);
            }

            return invoiceAutoNumber;
        }
        private InvoiceAutoNumberMonthly GetInvoiceAutoNumberMonthly()
        {
            string id = string.Format("{0}{1}", transactionDate.Year.ToString(), transactionDate.Month.ToString().PadLeft(2, '0'));
            MongoCollection<InvoiceAutoNumberMonthly> collection = mongo.MongoDatabase.GetCollection<InvoiceAutoNumberMonthly>(typeof(InvoiceAutoNumberMonthly).Name);
            InvoiceAutoNumberMonthly invoiceAutoNumber = collection.FindOneAs<InvoiceAutoNumberMonthly>(Query.And(
                Query.EQ("_id", BsonValue.Create(id)),
                Query.EQ(COMPANY_ID_FIELD, BsonValue.Create(companyId))));
            
            if (invoiceAutoNumber == null)
            {
                invoiceAutoNumber = new InvoiceAutoNumberMonthly(id, companyId, transactionDate.Year, transactionDate.Month);
                collection.Save<InvoiceAutoNumberMonthly>(invoiceAutoNumber);
            }

            return invoiceAutoNumber;
        }
        private InvoiceAutoNumberYearly GetInvoiceAutoNumberYearly()
        {
            MongoCollection<InvoiceAutoNumberYearly> collection = mongo.MongoDatabase.GetCollection<InvoiceAutoNumberYearly>(typeof(InvoiceAutoNumberYearly).Name);
            InvoiceAutoNumberYearly invoiceAutoNumber = collection.FindOneAs<InvoiceAutoNumberYearly>(Query.And(
                Query.EQ("_id", BsonValue.Create(transactionDate.Year.ToString())),
                Query.EQ(COMPANY_ID_FIELD, BsonValue.Create(companyId))));
            
            if (invoiceAutoNumber == null)
            {
                invoiceAutoNumber = new InvoiceAutoNumberYearly(transactionDate.Year.ToString(), companyId, transactionDate.Year);
                collection.Save<InvoiceAutoNumberYearly>(invoiceAutoNumber);
            }

            return invoiceAutoNumber;
        }

        public void SetupInvoiceAutoMumber(AutoNumberMode mode, string prefix, string companyId)
        {
           var config = GetInvoiceAutoNumberConfig(companyId);
           config.SetupAutoNumber(mode, prefix);
           MongoCollection<InvoiceAutoNumberConfig> collection = mongo.MongoDatabase.GetCollection<InvoiceAutoNumberConfig>(typeof(InvoiceAutoNumberConfig).Name);
           collection.Save<InvoiceAutoNumberConfig>(config);
        }
    }

    public static class InvoiceAutoNumberWriter
    {
        public static InvoiceAutoNumberDraft Save(this MongoConfig mongo, InvoiceAutoNumberDraft invAutoNumber)
        {
            mongo.MongoDatabase.GetCollection<InvoiceAutoNumberDraft>(typeof(InvoiceAutoNumberDraft).Name).
                Save<InvoiceAutoNumberDraft>(invAutoNumber);
            return invAutoNumber;
        }
        public static InvoiceAutoNumberDefault Save(this MongoConfig mongo, InvoiceAutoNumberDefault invAutoNumber)
        {
            mongo.MongoDatabase.GetCollection<InvoiceAutoNumberDefault>(typeof(InvoiceAutoNumberDefault).Name).
                Save<InvoiceAutoNumberDefault>(invAutoNumber);
            return invAutoNumber;
        }
        public static InvoiceAutoNumberYearly Save(this MongoConfig mongo, InvoiceAutoNumberYearly invAutoNumber)
        {
            mongo.MongoDatabase.GetCollection<InvoiceAutoNumberYearly>(typeof(InvoiceAutoNumberYearly).Name).
                Save<InvoiceAutoNumberYearly>(invAutoNumber);
            return invAutoNumber;
        }
        public static InvoiceAutoNumberMonthly Save(this MongoConfig mongo, InvoiceAutoNumberMonthly invAutoNumber)
        {
            mongo.MongoDatabase.GetCollection<InvoiceAutoNumberMonthly>(typeof(InvoiceAutoNumberMonthly).Name).
                Save<InvoiceAutoNumberMonthly>(invAutoNumber);
            return invAutoNumber;
        }
    }
}