using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.security.model;
using MongoDB.Driver;
namespace dokuku.security.repository
{
    public interface IAccountRepository
    {
        Account FindAccountByName(string userName);
        Account FindAccountByGuid(Guid guid);
    }
}