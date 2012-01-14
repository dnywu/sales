using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.repository;
using dokuku.security.model;

namespace dokuku.security.service
{
    public class AuthService : IAuthService
    {
        IAuthRepository _authRepo;
        public AuthService(IAuthRepository authrepository)
        {
            _authRepo = authrepository;
        }
        public Guid Login(string userName, string password)
        {
            if (_authRepo == null)
                throw new ApplicationException("Repository Account tidak ditemukan dalam container");
            return _authRepo.Login(userName, password);
        }
    }
}
