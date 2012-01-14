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
        MongoDatabase db;
        MongoCollection<Organization> collection;
        public OrganizationRepository()
        {
            db = MongoConfig.Instance.CommandDatabase;
            collection = db.GetCollection<Organization>("organizations");
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

        
    }
}