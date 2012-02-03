using Nancy;
using Nancy.Security;
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
            Get["/home"] = p =>
            {
                return View["webclient/sales/controllers/home/index"];
            };
            Get["/invoices"] = p =>
            {
                return View["webclient/sales/controllers/invoices/index"];
            };
            Get["/customer"] = p =>
            {
                return View["webclient/sales/controllers/customers/index"];
            };
            Get["/setting"] = p =>
            {
                return View["webclient/sales/controllers/setupautonumbering/index"];
            };
            Get["/getuser"] = p =>
            {
                return Response.AsJson(this.Context.CurrentUser.UserName);
            };
        }
    }
}