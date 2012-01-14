using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using LoveSeat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Text.RegularExpressions;
namespace dokuku.sales.item
{
    public class ItemRepository : IItemRepository
    {
        //CouchClient couchClient;
        //CouchDatabase db;
        //CouchDBConfig cfg;
        MongoCollection<BsonDocument> _document;

        public ItemRepository()
        {
            _document = MongoConfig.Instance.MongoDatabase.GetCollection(typeof(Item).Name);
            //cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            //if (cfg == null)
            //    throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config"); 
            //couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
        }

        public void Save(Item item)
        {
            _document.Insert(item.ToBsonDocument());
            //Document<Item> doc = new Document<Item>(item);
            //DB.SaveDocument(doc);
        }

        public Item Get(Guid id)
        {
            //return DB.GetDocument<Item>(id);
            return _document.FindOneAs<Item>(Query.EQ("_id", id));
        }

        public void Delete(Guid id)
        {
            //Item cs = DB.GetDocument<Item>(id);
            //if (cs == null)
            //    return;
            //DB.DeleteDocument(cs._id.ToString(), cs._rev);
            _document.Remove(Query.EQ("_id", id));
        }

        public IEnumerable<Item> AllItems(string companyId)
        {
            //return DB.View<Item>("all_items", "view_items").Items.Where(item =>
            //    {
            //        return item.OwnerId == companyId;
            //    });
            return _document.FindAs<Item>(Query.EQ("OwnerId", companyId));
        }

        public Item GetItemByName(string ownerId, string itemName)
        {
            var query = Query.And(Query.EQ("OwnerId", ownerId), Query.EQ("Name", BsonValue.Create(new Regex("^" + itemName + "$", RegexOptions.IgnoreCase)))); 
            return _document.FindOneAs<Item>(query);
        }

        public int CountItems(string ownerId)
        {
            //ViewResult<Item> result = DB.View<Item>("all_items", "view_items");
            //return result.Items.Where(m => m.OwnerId == ownerId).Count();
            return AllItems(ownerId).Count();
        }

        public IEnumerable<Item> LimitItems(string ownerId, int start, int limit)
        {
            //ViewResult<Item> result = DB.View<Item>("all_items", "view_items");
            //var filterResult =  result.Items.Where(m => m.OwnerId == ownerId).Skip(start).Take(limit);
            //return filterResult;
            MongoCursor<Item> mongoCursor = (MongoCursor<Item>)AllItems(ownerId);
            mongoCursor.Skip = start;
            mongoCursor = mongoCursor.SetLimit(limit);
            return mongoCursor;
        }

        //private CouchDatabase DB
        //{
        //    get
        //    {
        //        if (db == null)
        //            db = couchClient.GetDatabase(cfg.Database);
        //        return db;
        //    }
        //}
    }
}