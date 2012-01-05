using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoveSeat;
using dokuku.sales.config;
using System.Configuration;
namespace dokuku.sales.item
{
    public class ItemRepository : IItemRepository
    {
        CouchClient couchClient;
        CouchDatabase db;

        public ItemRepository()
        {
            CouchDBConfig cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            if (cfg == null)
                throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config"); 
            couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
        }

        public void Save(Item item)
        {
            Document<Item> doc = new Document<Item>(item);
            DB.SaveDocument(doc);
        }

        public Item Get(Guid id)
        {
            return DB.GetDocument<Item>(id);
        }

        public void Delete(Guid id)
        {
            Item cs = DB.GetDocument<Item>(id);
            if (cs == null)
                return;
            DB.DeleteDocument(cs._id.ToString(), cs._rev);
        }

        public IEnumerable<Item> AllItems()
        {
            return DB.View<Item>("all_items", "view_items").Items;
        }

        private CouchDatabase DB
        {
            get
            {
                if (db == null)
                    db = couchClient.GetDatabase("dokuku");
                return db;
            }
        }
    }
}