using Nancy;
using Nancy.Security;
using Nancy.ViewEngines.Razor;
using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using Common.Logging;
using dokuku.sales.web.models;
using dokuku.sales.organization;
namespace dokuku.sales.web.modules
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            this.RequiresAuthentication();
            IOrganizationRepository orgRepo = new OrganizationRepository();
            Get["/"] = p =>
                {
                    return View["webclient/sales/index"];
                };
            Get["/getorganization"] = p =>
                {
                    
                    return Response.AsJson(orgRepo.FindByOwnerId(this.Context.CurrentUser.UserName));
                };
            Get["/getuser"] = p =>
                {
                    return Response.AsJson(this.Context.CurrentUser.UserName);
                };
            Post["/setuporganization"] = p =>
                {
                    try
                    {
                        string name = (string)this.Request.Form.name;
                        string timezone = (string)this.Request.Form.timezone;
                        string curr = (string)this.Request.Form.curr;
                        int starts = (int)this.Request.Form.starts;
                        Guid id = Guid.NewGuid();
                        string owner = this.Context.CurrentUser.UserName;

                        orgRepo.Save(new Organization(id, owner, name, curr, starts));
                    }
                    catch (Exception ex)
                    {
                        return Context.GetRedirect("webclient/sales/index?error=true&message=" + ex.Message);
                    }
                    return View["webclient/sales/index"];
                };
        }
    }
}