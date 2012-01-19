using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB;
using System.Text.RegularExpressions;
using dokuku.sales.item.model;
namespace dokuku.sales.item
{
    public class ItemQuery : IItemQuery
    {
        MongoCollection<BsonDocument> _document;
        MongoCollection<BsonDocument> _reports;
        public ItemQuery(MongoConfig mongoConfig)
        {
            _document = mongoConfig.MongoDatabase.GetCollection(typeof(Item).Name);
            _reports = mongoConfig.MongoDatabase.GetCollection(typeof(ItemReports).Name);
        }

        public Item Get(Guid id)
        {
            return _document.FindOneAs<Item>(Query.EQ("_id", id));
        }

        public IEnumerable<Item> AllItems(string companyId)
        {
            return _document.FindAs<Item>(Query.EQ("OwnerId", companyId));
        }

        public long CountItems(string ownerId)
        {
            return _document.Count(Query.EQ("OwnerId", ownerId));
        }

        public IEnumerable<Item> LimitItems(string ownerId, int start, int limit)
        {
            MongoCursor<Item> mongoCursor = (MongoCursor<Item>)AllItems(ownerId);
            mongoCursor = mongoCursor.SetSkip(start).SetLimit(limit);
            return mongoCursor;
        }

        public Item GetItemByName(string ownerId, string itemName)
        {
            return _document.FindOneAs<Item>(Query.And(Query.EQ("OwnerId", ownerId),
                                             Query.EQ("Name", new Regex(itemName, RegexOptions.IgnoreCase))));
        }

        public IEnumerable<ItemReports> Search(string ownerId, String[] keywords)
        {
            var qry = Query.And(Query.EQ("OwnerId", BsonValue.Create(ownerId)), getQuery(keywords));
            return _reports.FindAs<ItemReports>(qry).SetLimit(10);
        }

        private QueryComplete getQuery(string[] keywords)
        {
            QueryComplete[] qries = new QueryComplete[keywords.Length];
            int index = 0;
            foreach (string keyword in keywords)
            {
                qries[index] = Query.EQ("Keywords", new Regex(keyword, RegexOptions.IgnoreCase));
                index++;
            }
            return Query.Or(qries);
        }

        public Item FindByBarcode(string barcode, string owner)
        {
            return _document.FindOneAs<Item>(Query.And(
                Query.EQ("OwnerId", owner),
                Query.EQ("Barcode", barcode == null ? string.Empty : barcode)));
        }

        public Item FindByCode(string code, string owner)
        {
            return _document.FindOneAs<Item>(Query.And(
                Query.EQ("OwnerId", owner),
                Query.EQ("Code", code == null ? string.Empty : code)));
        }
        public bool IsCodeAlreadyExist(string code, string owner)
        {
            if (FindByCode(code, owner) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsBarcodeAlreadyExist(string barcode, string owner)
        {
            if (FindByBarcode(barcode, owner) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}