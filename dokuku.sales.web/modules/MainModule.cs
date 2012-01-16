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
using dokuku.sales.customer;
using dokuku.security;
using Newtonsoft.Json;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.report;
using dokuku.sales.organization.model;
using dokuku.sales.customer.model;
using dokuku.sales.customer.repository;
using dokuku.sales.item;
using dokuku.security.model;
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
            
            Get["/getuser"] = p =>
                {
                    return Response.AsJson(this.Context.CurrentUser.UserName);
                };
        }
    }
}