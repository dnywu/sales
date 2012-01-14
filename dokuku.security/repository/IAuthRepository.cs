using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.model;

namespace dokuku.security.repository
{
    public interface IAuthRepository
    {
        Guid Login(string userName, string password);
    }
}
