using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using System.Collections;
using System.Collections.Generic;
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
                    return Response.AsJson(this.TaxQueryRepository().GetAllTaxes(this.CurrentAccount().OwnerId));
                };
            Post["/SaveTax"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    return Response.AsJson(this.ServiceTax().Create(data, this.CurrentAccount().OwnerId));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }

            };
            Delete["/DeleteTax/{id}"] = p =>
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
            Get["/GetTaxById/{id}"] = p =>
            {
                Guid id = p.id;
                return Response.AsJson(this.TaxQueryRepository().GetTaxById(id, this.CurrentAccount().OwnerId));
            };
            Post["/UpdateTax"] = p =>
            {
                string Data = this.Request.Form.data;
                try
                {
                    this.ServiceTax().Update(Data, this.CurrentAccount().OwnerId);
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