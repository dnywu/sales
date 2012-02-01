using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using dokuku.sales.config;
using dokuku.sales.organization.model;
using MongoDB.Driver.Builders;

namespace dokuku.sales.organization.report
{
    public class LogoOrganizationQuery : ILogoOrganizationQuery
    {
        MongoCollection _collection;

        public LogoOrganizationQuery(MongoConfig config)
        {
            _collection = config.MongoDatabase.GetCollection(typeof(LogoOrganization).Name);
        }
        public LogoOrganization GetLogo(string id)
        {
            return _collection.FindOneAs<LogoOrganization>(Query.EQ("_id", id));
        }
    }
}
