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
        MongoDatabase reportingDB;
        MongoCollection<Organization> reportCollections;
        public OrganizationReportRepository()
        {
            reportingDB = MongoConfig.Instance.ReportingDatabase;
            reportCollections = reportingDB.GetCollection<Organization>("organizations");
        }
        public Organization FindByOwnerId(string email)
        {
            QueryDocument qry = new QueryDocument(){
                                    {"OwnerId",email}};
            return reportCollections.Find(qry).FirstOrDefault();
        }
    }
}
