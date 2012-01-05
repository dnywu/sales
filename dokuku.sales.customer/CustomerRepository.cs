using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoveSeat;
using dokuku.sales.config;
using System.Configuration;
namespace dokuku.sales.customer
{
    public class CustomerRepository : ICustomerRepository
    {
        CouchClient couchClient;
        CouchDatabase db;

        public CustomerRepository()
        {
            CouchDBConfig cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            if (cfg == null)
                throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config"); 
            couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
        }

        public void Save(Customer cs)
        {
            Document<Customer> doc = new Document<Customer>(cs);
            DB.SaveDocument(doc);
        }
        
        public Customer Get(Guid id)
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

        public IEnumerable<Customer> AllCustomers()
        {
            return DB.View<Customer>("all_customers", "view_customers").Items;
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