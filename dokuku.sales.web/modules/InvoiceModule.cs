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
                try
                {
                    Invoices result = this.InvoiceService().Create(this.Request.Form.invoice, this.Context.CurrentUser.UserName);
                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new {error= true, message = ex.Message});
                }
            };
            Get["/GetDataInvoice"] = p =>
                {
                    Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                    return Response.AsJson(this.InvoicesQueryRepository().AllInvoices(account.OwnerId));
                };
            Get["/GetDataInvoiceByInvoiceID/_id/{id}"] = p =>
            {
                string id = p.id;
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                var invoices = this.InvoicesRepository().Get(id, account.OwnerId);
                return Response.AsJson(invoices);
            };
            Post["/UpdateInvoice"] = p =>
            {
                try
                {
                    Invoices result = this.InvoiceService().Update(this.Request.Form.invoice, this.Context.CurrentUser.UserName);
                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/invoice/{id}"] = p =>
                {
                    Guid invoiceId = p.id;
                    Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                    Invoices invoice = this.InvoicesQueryRepository().FindById(invoiceId, account.OwnerId);
                    return Response.AsJson(invoice);
                };
        }
    }
}