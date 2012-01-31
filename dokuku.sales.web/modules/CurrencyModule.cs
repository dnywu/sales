using System;
using System.Collections;
using System.Collections.Generic;
using dokuku.security.model;
using Nancy;
using Nancy.Security;

namespace dokuku.sales.web.modules
{
    public class CurrencyModule : Nancy.NancyModule
    {
        public CurrencyModule()
        {
            this.RequiresAuthentication();
            Get["/GetAllCurrency"] = p =>
            {
                return Response.AsJson(this.CurencyQueryRepo().GetAllCurrency(this.CurrentAccount().OwnerId));
            };
            Post["/SaveCurrency"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    return Response.AsJson(this.CurrencyService().Create(data, this.CurrentAccount().OwnerId));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }

            };
            Delete["/DeleteCurrency/{id}"] = p =>
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
            Get["/GetDataCurrency/{id}"] = p =>
            {
                Guid id = p.id;
                return Response.AsJson(this.CurencyQueryRepo().GetCurrencyById(id, this.CurrentAccount().OwnerId));
            };
            Post["/UpdateDataCurrency"] = p =>
            {
                string Data = this.Request.Form.data;
                try
                {
                    this.CurrencyService().UpdateCurrency(Data,this.CurrentAccount()._id);
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