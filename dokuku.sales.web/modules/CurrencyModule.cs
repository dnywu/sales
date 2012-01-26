using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using System.Collections;
using System.Collections.Generic;
using dokuku.security.model;

namespace dokuku.sales.web.modules
{
    public class CurrencyModule : Nancy.NancyModule
    {
        public CurrencyModule()
        {
            this.RequiresAuthentication();
            Get["/GetAllCurrency"] = p =>
            {
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                return Response.AsJson(this.CurencyQueryRepo().GetAllTaxes(account.OwnerId));
            };
            Post["/SaveCurrency"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    return Response.AsJson(this.CurrencyService().Create(data, this.Context.CurrentUser.UserName));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }

            };
            Get["/DeleteCurrency/{id}"] = p =>
            {
                try
                {
                    Guid id = p.id;
                    this.CurrencyService().Delete(id);
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsJson("OK");
            };
        }
    }
}