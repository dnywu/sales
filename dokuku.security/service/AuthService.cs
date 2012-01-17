using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.repository;
using dokuku.security.model;
using dokuku.security.models;
namespace dokuku.security.service
{
    public class AuthService : IAuthService
    {
        IAccountRepository accRepo;
        public AuthService(IAccountRepository accRepo)
        {
            this.accRepo = accRepo;
        }
        public Guid Login(string userName, string password)
        {
            if (accRepo == null)
                throw new ApplicationException("Repository Account tidak ditemukan dalam container");
            Account account = accRepo.FindAccountByName(userName);
            if(account == null || account.Password != password)
                throw new InvalidUsernameOrPasswordException();
            return account.Guid;
        }
    }
}