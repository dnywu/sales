using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace dokuku.security.models
{
    public class InvalidUsernameOrPasswordException : ApplicationException
    {
        public InvalidUsernameOrPasswordException()
            : base("Username atau password anda salah")
        {
        }
    }
}