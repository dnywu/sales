using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.security.model;
using Nancy;

namespace dokuku.sales.web.modules
{
    public static class ModuleExtension
    {
        public static Account CurrentAccount(this NancyModule module)
        {
            Account account = module.AccountRepository().FindAccountByName(module.Context.CurrentUser.UserName);
            if (account == null)
                throw new Exception(String.Format("Account untuk user '{0}' tidak dapat ditemukan.", module.Context.CurrentUser.UserName));
            return account;
        }
    }
}