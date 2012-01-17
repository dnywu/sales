using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.organization.model;
using MongoDB.Driver;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
namespace dokuku.sales.organization.report
{
    public class OrganizationReportRepository : IOrganizationReportRepository
    {
        MongoConfig mongo;
        public OrganizationReportRepository(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }
        public Organization FindByOwnerId(string email)
        {
            return reportCollections.FindOneAs<Organization>(Query.EQ("_id", BsonValue.Create(email)));
        }
        private MongoCollection<Organization> reportCollections
        {
            get { return mongo.ReportingDatabase.GetCollection<Organization>("organizations"); }
        }
    }
}