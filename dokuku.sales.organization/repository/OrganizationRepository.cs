using dokuku.sales.config;
using System.Configuration;
using System;
using System.Linq;
using dokuku.sales.organization.model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.organization.repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        MongoConfig mongo;
        public OrganizationRepository(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }

        public void Save(Organization org)
        {
            collection.Save<Organization>(org);
        }

        public void Delete(string id)
        {
            collection.Remove(Query.EQ("_id", id));
        }

        private MongoCollection<Organization> collection
        {
            get { return mongo.MongoDatabase.GetCollection<Organization>(typeof(Organization).Name); }
        }
    }
}