using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.organization.model;
using MongoDB.Driver;
using dokuku.sales.config;
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
            QueryDocument qry = new QueryDocument(){
                                    {"OwnerId",email}};
            return reportCollections.FindOneAs<Organization>(qry);
        }
        private MongoDatabase db
        {
            get { return mongo.MongoDatabase; }
        }
        private MongoCollection<Organization> reportCollections
        {
            get { return db.GetCollection<Organization>("organizations"); }
        }
    }
}
