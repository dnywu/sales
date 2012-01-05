using LoveSeat;
using dokuku.sales.config;
using System.Configuration;
using System;
using System.Linq;
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

        public void Save(Organization org)
        {
            Document<Organization> doc = new Document<Organization>(org);
            DB.SaveDocument(doc);
        }

        public void Delete(Guid id)
        {
            Organization orgRecord = DB.GetDocument<Organization>(id);
            if (orgRecord == null)
                return;
            DB.DeleteDocument(orgRecord._id.ToString(), orgRecord._rev);
        }

        public Organization Get(Guid id)
        {
            return DB.GetDocument<Organization>(id);
        }

        public Organization FindByOwnerId(string email)
        {
            ViewResult<Organization> result = DB.View<Organization>("all_docs", "view_organizations");
            return result.Items.Where(o => o.OwnerId == email).FirstOrDefault();
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