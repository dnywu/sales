using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using System.Collections;
using System.Collections.Generic;
using dokuku.security.model;
using dokuku.sales.taxes.model;
namespace dokuku.sales.web.modules
{
    public class TaxModule : Nancy.NancyModule
    {
        public TaxModule()
        {
            this.RequiresAuthentication();
            Get["/GetAllTax"] = p =>
                {
                    Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                    return Response.AsJson(this.TaxQueryRepository().GetAllTaxes(account.OwnerId));
                };
            Post["/SaveTax"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    return Response.AsJson(this.ServiceTax().Create(data, this.Context.CurrentUser.UserName));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }

            };
            Get["/DeleteTax/{id}"] = p =>
                {
                    try
                    {
                        Guid id = p.id;
                        this.ServiceTax().Delete(id);
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