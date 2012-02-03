using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using Nancy;
using Nancy.Responses;
using dokuku.sales.web.models;
using dokuku.security.repository;
using dokuku.security.model;
using dokuku.security.models;
namespace dokuku.sales.web.modules
{
    public class AuthModule : Nancy.NancyModule
    {
        public AuthModule()
        {
            Get["/scripts/{file}"] = p =>
            {
                string filename = p.file.ToString();
                return Response.AsJs("Views/scripts/" + filename);
            };

            Get["/steal/{file}"] = p =>
            {
                string filename = p.file.ToString();
                return Response.AsJs("webclient/steal/" + filename);
            };

            Get["/Content/{file}"] = p =>
            {
                string filename = p.file.ToString();
                return Response.AsJs("Content/" + filename);
            };

            Get["/jquery/{path}"] = p =>
            {
                string path = p.path.ToString();
                return Response.AsJs("webclient/jquery/" + path);
            };

            Get["/sales/{path}"] = p =>
            {
                string path = p.path.ToString();
                return Response.AsJs("webclient/sales/" + path);
            };

            Get["/funcunit/{path}"] = p =>
            {
                string path = p.path.ToString();
                return Response.AsJs("webclient/funcunit/" + path);
            };

            Get["/Views/Image"] = p =>
                {
                    string filename = p.file.ToString();
                    return Response.AsImage("Views/Image/" + filename);
                };

            Get["/css/{file}"] = p =>
            {
                string filename = p.file.ToString();
                return Response.AsCss("Views/css/" + filename);
            };
            
            Get["/login"] = p =>
            {
                return View["/login"];
            };

            Post["/login"] = x =>
            {
                try
                {
                    var userGuid = ServiceLocator.GetAuthenticationService(this).Login((string)this.Request.Form.Username, (string)this.Request.Form.Password);
                    if (userGuid == null)
                    {
                        return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                    }

                    DateTime? expiry = null;
                    if (this.Request.Form.RememberMe.HasValue)
                    {
                        expiry = DateTime.Now.AddDays(7);
                    }

                    return this.LoginAndRedirect(userGuid, expiry);
                }
                catch (InvalidUsernameOrPasswordException ex)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            };

            Get["/logout"] = x =>
            {
                return this.LogoutAndRedirect("~/");
            };

            Get["/getorganization"] = p =>
            {
                return Response.AsJson(this.OrganizationReportRepository().FindByOwnerId(this.CurrentAccount().OwnerId));
            };

            Get["/validatesetuporganization"] = p =>
            {
                Account acc = this.CurrentAccount();
                return Response.AsJson(new { IsValid = acc.IsOwner() });
            };
        }
    }
}