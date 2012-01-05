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

            Get["/"] = p =>
                {
                    return View["webclient/sales/index"];
                };
            Get["/getorganization"] = p =>
                {
                    IOrganizationRepository organization = new OrganizationRepository();
                    return Response.AsJson(organization.FindByOwnerId(this.Context.CurrentUser.UserName));
                };
            Get["/getuser"] = p =>
                {
                    return Response.AsJson(this.Context.CurrentUser.UserName);
                };
            Post["/setuporganization"] = p =>
                {

                };
        }
    }
}