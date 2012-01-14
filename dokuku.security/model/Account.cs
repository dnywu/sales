using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.security.model
{
    public class Account
    {
        public const string OWNER = "owner";
        public const string USER = "user";
        public const string ACTIVE = "active";
        public const string INVITED = "invited";

        public string _id { get; private set; }
        public string Password { get; private set; }
        public string OwnerId { get; private set; }
        public string[] Roles { get; private set; }
        public string Status { get; private set; }
        public Guid Guid { get; private set; }

        private Account() { }

        public static Account AsOwner(string username, string password)
        {
            return new Account()
            {
                _id = username,
                OwnerId = username,
                Password = password,
                Roles = new string[1] { OWNER },
                Status = ACTIVE,
                Guid = Guid.NewGuid()
            };
        }

        public static Account AsUser(string username, string password, string ownerid)
        {
            return new Account()
            {
                _id = username,
                OwnerId = ownerid,
                Password = password,
                Roles = new string[1] { USER },
                Status = INVITED,
                Guid = Guid.NewGuid()
            };
        }

        public void ChangePassword(string oldPass, string newPass)
        {
            if (oldPass != Password)
                throw new ApplicationException("Password tidak cocok");
            Password = newPass;
        }

        public void ChangeRoleToUser(string oldPass)
        {
            if (oldPass != Password)
                throw new ApplicationException("Password tidak cocok");
            Roles = new string[1] { USER };
        }

        public void ChangeRoleToOwner(string oldPass)
        {
            if (oldPass != Password)
                throw new ApplicationException("Password tidak cocok");
            Roles = new string[1] { OWNER };
        }

        public void ActivateUser()
        {
            Status = ACTIVE;
        }
    }
}
