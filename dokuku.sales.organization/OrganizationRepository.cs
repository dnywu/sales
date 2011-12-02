using LoveSeat;
using dokuku.sales.config;
using System.Configuration;
using System;
namespace dokuku.sales.organization
{
    public class OrganizationRepository : IOrganizationRepository
    {
        CouchClient couchClient;
        CouchDatabase db;
        
        public OrganizationRepository()
        {
            CouchDBConfig cfg = (CouchDBConfig)ConfigurationManager.GetSection("CouchDBConfig");
            if (cfg == null)
                throw new ApplicationException("CouchDBConfig tidak di temukan dalam app config"); 
            couchClient = new CouchClient(cfg.Server, cfg.Port, cfg.Username, cfg.Password, false, AuthenticationType.Basic);
        }

        public Organization Create(Organization org)
        {
            Document<Organization> doc = new Document<Organization>(org);
            DB.CreateDocument(doc);
            return DB.GetDocument<Organization>(org._id);
        }

        public void Delete(Organization org)
        {
            DB.DeleteDocument(org._id, org._rev);
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