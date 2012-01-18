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
using MongoDB.Driver.Builders;
using System.Text.RegularExpressions;
namespace dokuku.sales.item
{
    public class ItemQuery : IItemQuery
    {
        MongoCollection<BsonDocument> _document;
        public ItemQuery(MongoConfig mongoConfig)
        {
            _document = mongoConfig.MongoDatabase.GetCollection(typeof(Item).Name);
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
                                             Query.EQ("Name", new Regex("^" + itemName + "$", RegexOptions.IgnoreCase))));
        }

        #region IItemQuery Members


        public Item FindByBarcode(string barcode, string owner)
        {
            return _document.FindOneAs<Item>(Query.And(
                Query.EQ("OwnerId", owner),
                Query.EQ("Barcode",barcode)));
        }

        public Item FindByCode(string code, string owner)
        {
            return _document.FindOneAs<Item>(Query.And(
                Query.EQ("OwnerId", owner),
                Query.EQ("Code", code)));
        }

        #endregion
    }
}