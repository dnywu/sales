using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.model;
using MongoDB.Driver;
using dokuku.sales.config;

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
            QueryDocument qry = new QueryDocument(){{"OwnerId", userName}};
            return Collections.FindOneAs<Account>(qry);
        }

        public Account FindAccountByGuid(Guid guid)
        {
            QueryDocument qryDoc = new QueryDocument();
            qryDoc["Guid"] = guid;
            return Collections.FindOneAs<Account>(qryDoc);
        }

        private MongoCollection<Account> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Account>("accounts"); }
        }
    }
}