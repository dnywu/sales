using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Authentication.Forms;
using dokuku.security.repository;
using Nancy.Security;
namespace dokuku.security.model
{
    public class UserMapper : IUserMapper
    {
        IAccountRepository accRepo;
        public UserMapper(IAccountRepository accRepo)
        {
            this.accRepo = accRepo;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier)
        {
            Account userRecord = accRepo.FindAccountByGuid(identifier);
            return userRecord == null
                       ? null
                       : new UserIdentity { UserName = userRecord._id, Claims = userRecord.Roles };
        }
    }
}