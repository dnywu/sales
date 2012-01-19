using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.security.model
{
    public class Account
    {
        public const string OWNER = "owner";
        public const string ADMIN = "admin";
        public const string USER = "user";
        public const string ACTIVE = "active";
        public const string INVITED = "invited";

        public string _id { get; private set; }
        public string Password { get; private set; }
        public string OwnerId { get; private set; }
        public string[] Roles { get; private set; }
        public string Status { get; private set; }
        public Guid Guid { get; private set; }
        public bool IsOwner()
        {
            return Roles.Contains(OWNER);
        }
    }
}