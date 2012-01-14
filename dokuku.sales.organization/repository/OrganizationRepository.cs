using dokuku.sales.config;
using System.Configuration;
using System;
using System.Linq;
using dokuku.sales.organization.model;
using MongoDB.Driver;
namespace dokuku.sales.organization.repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        MongoConfig mongo;
        public OrganizationRepository(MongoConfig mongoConfig)
        {
<<<<<<< HEAD
            mongo = mongoConfig;
=======
            db = MongoConfig.Instance.CommandDatabase;
            collection = db.GetCollection<Organization>("organizations");
>>>>>>> e8a8d296f06a23bbb10a177812d54e35f1e49448
        }

        public void Save(Organization org)
        {
            collection.Save<Organization>(org);
        }

        public void Delete(Guid id)
        {
            QueryDocument qryDoc = new QueryDocument();
            qryDoc["_id"] = id;
            collection.Remove(qryDoc);
        }

        public Organization Get(Guid id)
        {
            QueryDocument qry = new QueryDocument() {
                                    {"_id",id}};
            return collection.Find(qry).FirstOrDefault();
        }
        private MongoDatabase db
        {
            get { return mongo.MongoDatabase; }
        }
        private MongoCollection<Organization> collection
        {
            get { return db.GetCollection<Organization>("organizations"); }
        }

        
    }
}