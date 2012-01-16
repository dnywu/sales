using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.model;
using MongoDB.Driver;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Dynamic;
using Newtonsoft.Json.Bson;
namespace dokuku.security.repository
{
    public class AccountRepository : IAccountRepository
    {
        MongoConfig mongo;
        public AccountRepository(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }

        public Account FindAccountByName(string userName)
        {
            return Collections.FindOneAs<Account>(Query.EQ("_id",BsonValue.Create(userName)));
        }

        public Account FindAccountByGuid(Guid guid)
        {
            return Collections.FindOneAs<Account>(Query.EQ("Guid", BsonValue.Create(guid)));
        }

        private MongoCollection<Account> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Account>("accounts"); }
        }
    }
}