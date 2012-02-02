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

            Get["/uploadlogo"] = p =>
            {
                return View["/uploadlogo"];
            };

            Get["/getuser"] = p =>
            {
                return Response.AsJson(this.Context.CurrentUser.UserName);
            };
        }
    }
}