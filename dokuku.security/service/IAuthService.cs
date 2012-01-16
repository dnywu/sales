using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.security.service
{
    public interface IAuthService
    {
        Guid Login(string userName, string password);
    }
}
