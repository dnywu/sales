using Nancy;
using Nancy.Security;
using Nancy.ViewEngines.Razor;
using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using Common.Logging;
using dokuku.sales.web.models;
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
        }
    }
}