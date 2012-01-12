namespace dokuku.security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nancy.Authentication.Forms;
    using Nancy.Security;

    using LoveSeat;
    using LoveSeat.Interfaces;

    public class AuthRepository : IUserMapper
    {
        const string ADMIN_ROLE = "admin";
        const string OWNER_ROLE = "owner";
        const string ACCOUNT_TYPE = "account";
        static CouchClient couchClient;
        static CouchDatabase db;

        static AuthRepository()
        {
            couchClient = new CouchClient("tcloud1.bonastoco.com", 5984, "admin", "S31panas", false, AuthenticationType.Basic);
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier)
        {
            CouchDatabase db = couchClient.GetDatabase("dokuku");
            ViewResult<Account> users = db.View<Account>("all_accounts", "view_accounts");
            var userRecord = users.Items.Where(acc => acc.Guid == identifier).FirstOrDefault();

            return userRecord == null
                       ? null
                       : new UserIdentity { UserName = userRecord.Email, Claims = userRecord.Roles };
        }

        public static IUserIdentity GetUserFromUsername(string username)
        {
            CouchDatabase db = couchClient.GetDatabase("dokuku");
            Account acc = db.GetDocument<Account>(username);
            
            return acc == null
                       ? null
                       : new UserIdentity { UserName = acc._id, Claims = acc.Roles };
        }

        public static Guid? ValidateUser(string email, string password)
        {
            CouchDatabase db = couchClient.GetDatabase("dokuku");
            Account acc = db.GetDocument<Account>(email);
            if (acc == null)
                return null;
            return acc.Guid;
        }

        public static Guid? SignUp(string email, string password)
        {
            CouchDatabase db = couchClient.GetDatabase("dokuku");
            Account acc = db.GetDocument<Account>(email);
            if(acc!=null)
                throw new ApplicationException(String.Format("{0} sudah terdaftar", acc._id));

            Document<Account> account = new Document<Account>(new Account
            {
                _id = email,
                Guid = Guid.NewGuid(),
                Email = email,
                Password = password,
                Roles = new string[2] { OWNER_ROLE, ADMIN_ROLE },
                Type = ACCOUNT_TYPE
            });

            db.CreateDocument(account);

            return ValidateUser(email, password);
        }

        public static AccountUser GetAccountByUsername(string username)
        {
            CouchDatabase db = couchClient.GetDatabase("dokuku");
            AccountUser accUser = db.GetDocument<AccountUser>(username);
            return accUser;
        }

        public class Account
        {
            public string _id { get; set; }
            public string _rev { get; set; }
            public Guid Guid { get; set; }
            public string Email {get;set;}
            public string Password { get; set; }
            public string[] Roles { get; set; }
            public string Type { get; set; }
        }

        public class AccountUser : Account
        {
            public string CompanyId { get; set; }
        }
    }
}