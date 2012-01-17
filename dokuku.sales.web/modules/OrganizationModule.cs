using Nancy;
using Nancy.Security;
using System;
using dokuku.sales.organization.model;
namespace dokuku.sales.web.modules
{
    public class OrganizationModule : Nancy.NancyModule
    {
        public OrganizationModule()
        {
            this.RequiresAuthentication();

            Get["/getorganization"] = p =>
            {
                return Response.AsJson(this.OrganizationReportRepository().FindByOwnerId(this.Context.CurrentUser.UserName));
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
                    this.OrganizationRepository().Save(new Organization(id, owner, name, curr, starts));
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsRedirect("/");
            };
        }
    }
}