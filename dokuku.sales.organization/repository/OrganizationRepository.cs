using dokuku.sales.config;
using System.Configuration;
using System;
using System.Linq;
using dokuku.sales.organization.model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
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
            collection.Remove(Query.EQ("_id",BsonValue.Create(id)));
        }

        public Organization Get(string id)
        {
            return collection.FindOneAs<Organization>(Query.EQ("_id", BsonValue.Create(id)));
        }
        private MongoCollection<Organization> collection
        {
            get { return mongo.MongoDatabase.GetCollection<Organization>("organizations"); }
        }
    }
}