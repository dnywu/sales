using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.model;
using MongoDB.Driver;
using dokuku.sales.config;

namespace dokuku.security.repository
{
    public class AuthRepository : IAuthRepository
    {
        MongoConfig mongo;
        public AuthRepository(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }

        public Guid Login(string userName, string password)
        {
            MongoDatabase db = mongo.MongoDatabase;
            MongoCollection<Account> collections = db.GetCollection<Account>("accounts");
            QueryDocument qry = new QueryDocument(){
                                   {"OwnerId", userName},
                                   {"Password", password}};
            Account account = collections.FindOneAs<Account>(qry);
            if (account == null)
                throw new ApplicationException("User Name ini tidak terdaftar!");
            return Guid.Parse(account._id);
        }
    }
}
