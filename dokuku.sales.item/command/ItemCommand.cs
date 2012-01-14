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
namespace dokuku.sales.item
{
    public class ItemCommand : IItemCommand
    {
        //CouchClient couchClient;
        //CouchDatabase db;
        //CouchDBConfig cfg;
        MongoCollection<BsonDocument> _document;

        public ItemCommand()
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

        public void Delete(Guid id)
        {
            //Item cs = DB.GetDocument<Item>(id);
            //if (cs == null)
            //    return;
            //DB.DeleteDocument(cs._id.ToString(), cs._rev);
            _document.Remove(Query.EQ("_id", id));
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