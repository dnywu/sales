using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;

namespace dokuku.sales.web.modules
{
    public class InvoiceModule: Nancy.NancyModule
    {
        public InvoiceModule()
        {
            this.RequiresAuthentication();
            Post["/createinvoice"] = p =>
            {
                Invoices invoice = null;
                try
                {
                    string data = this.Request.Form.invoice;
                    invoice = JsonConvert.DeserializeObject<Invoices>(data);
                    invoice._id = Guid.NewGuid();
                    invoice.OwnerId = this.Context.CurrentUser.UserName;
                    invoice.InvoiceNo = "Inv-01";
                    this.InvoicesRepository().Save(invoice);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new {error= true, message = ex.Message});
                }
                return Response.AsJson(new {noInvoice = invoice.InvoiceNo });
            };
        }
    }
}