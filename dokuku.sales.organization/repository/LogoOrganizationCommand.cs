using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Driver;
using dokuku.sales.organization.model;
using MongoDB.Driver.Builders;

namespace dokuku.sales.organization.repository
{
    public class LogoOrganizationCommand : ILogoOrganizationCommand
    {
        MongoCollection _collection;

        public LogoOrganizationCommand(MongoConfig config)
        {
            _collection = config.MongoDatabase.GetCollection(typeof(LogoOrganization).Name);
        }
        public void Save(LogoOrganization logo)
        {
            _collection.Save(logo);
        }
        public void Delete(string id)
        {
            _collection.Remove(Query.EQ("_id", id));
        }
    }
}
