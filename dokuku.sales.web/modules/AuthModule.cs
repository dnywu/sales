using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using Nancy;
using Nancy.Responses;
using dokuku.sales.web.models;
using dokuku.security.repository;
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

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = x =>
            {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}