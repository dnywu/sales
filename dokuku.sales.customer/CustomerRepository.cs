using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoveSeat;
using dokuku.sales.config;
using System.Configuration;
using MongoDB.Driver;
namespace dokuku.sales.customer
{
    public class CustomerRepository : ICustomerRepository
    {
        CouchClient couchClient;
        CouchDatabase db;
        CouchDBConfig cfg;
        //MongoDatabase db;
        public CustomerRepository()
        {
            cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            if (cfg == null)
                throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config");
            couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
            MongoDatabase db = MongoConfig.Instance.MongoDatabase;
        }

        public void Save(Customer cs)
        {
            Document<Customer> doc = new Document<Customer>(cs);
            DB.SaveDocument(doc);
            //db.GetCollection("dokuku");
        }
        
        public Customer Get(Guid id,string ownerId)
        {
            return DB.GetDocument<Customer>(id);
        }

        public void Delete(Guid id)
        {
            Customer cs = DB.GetDocument<Customer>(id);
            if (cs == null)
                return;
            DB.DeleteDocument(cs._id.ToString(), cs._rev);
        }

        public IEnumerable<Customer> LimitCustomers(string ownerId, int start, int limit)
        {
            ViewOptions ops = new ViewOptions();
            ops.StartKey = new KeyOptions(new object[1] { new string[2] { ownerId, "" }});
            ops.EndKey = new KeyOptions(new object[1] { new string[2] { ownerId + "\u9999", "" } });
            ops.Limit = limit;
            ops.Skip = start - (start > 0 ? 1 : 0);
            DB.SetDefaultDesignDoc("view_customers");
            return  DB.View<Customer>("all_customers", ops).Items;
        }
        public int CountCustomers(string ownerId)
        {
            ViewResult<Customer> result = DB.View<Customer>("all_customers", "view_customers");
            return result.Items.Where(m => m.OwnerId == ownerId).Count();
        }

        private CouchDatabase DB
        {
            get
            {
                if (db == null)
                    db = couchClient.GetDatabase(cfg.Database);
                return db;
            }
        }


        public Customer Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}