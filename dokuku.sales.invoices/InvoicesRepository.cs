using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoveSeat;
using dokuku.sales.config;
using System.Configuration;

namespace dokuku.sales.invoices
{
    public class InvoicesRepository : IInvoicesRepository
    {
        CouchClient couchClient;
        CouchDatabase db;
        CouchDBConfig cfg;

        public InvoicesRepository()
        {
            cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            if (cfg == null)
                throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config");
            couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
        }

        public void Save(Invoices ci)
        {
            Document<Invoices> doc = new Document<Invoices>(ci);
            DB.SaveDocument(doc);
        }

        public Invoices Get(Guid id)
        { 
            return DB.GetDocument<Invoices>(id);
        }

        public void Delete(Guid id)
        {
            Invoices ci = DB.GetDocument<Invoices>(id);
            if (ci == null)
                return;
            DB.DeleteDocument(ci._id.ToString(), ci._rev);
        }

        public IEnumerable<Invoices> AllInvoices()
        {
            return DB.View<Invoices>("all_invoices","view_invoices").Items;
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
    }
}
