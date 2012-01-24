using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace dokuku.sales.web.modules
{
    public class InvoiceModule : Nancy.NancyModule
    {
        public InvoiceModule()
        {
            this.RequiresAuthentication();
            Post["/createinvoice"] = p =>
            {
                try
                {
                    Invoices result = this.InvoiceService().Create(this.Request.Form.invoice, this.CurrentAccount().OwnerId);
                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/GetDataInvoice"] = p =>
            {
                return Response.AsJson(this.InvoicesQueryRepository().AllInvoices(this.CurrentAccount().OwnerId));
            };
            Delete["/deleteInvoice/invoiceNo/{invoiceId}"] = p =>
                {
                    try
                    {
                        Guid _id = Guid.Parse(p.invoiceId);
                        this.InvoiceService().Delete(_id, this.Context.CurrentUser.UserName);
                    }
                    catch (Exception ex)
                    {
                        return Response.AsJson(new { error = true, message = ex.Message });
                    }
                    return Response.AsJson("OK");
                };
            Get["/SearchInvoice/key/{key}"] = p =>
                {
                    string key = p.key;
                    IList<Invoices> invoices = new List<Invoices>();
                    IEnumerable<InvoiceReports> invoiceReport = this.InvoicesQueryRepository().Search(this.CurrentAccount().OwnerId, new string[] { key });
                    foreach (InvoiceReports invoice in invoiceReport)
                    {
                        invoices.Add(this.InvoicesRepository().Get(invoice._id, invoice.OwnerId));
                    }
                    return Response.AsJson(invoices);
                };
            Post["/UpdateInvoice"] = p =>
            {
                try
                {
                    this.InvoiceService().Update(this.Request.Form.invoice, this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { error = false });
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/invoice/{id}"] = p =>
                {
                    Guid invoiceId = p.id;
                    Invoices invoice = this.InvoicesQueryRepository().FindById(invoiceId, this.CurrentAccount().OwnerId);
                    return Response.AsJson(invoice);
                };
            Post["/approveinvoice/{id}"] = p =>
            {
                try
                {
                    this.InvoiceService().ApproveInvoice(p.id, this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { error = false });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e.Message });
                }
            };

            Get["/InvoiceAutoNumber"] = p =>
                {
                    var autoNumber = this.InvoiceAutoNumberGenerator().GetInvoiceAutoNumberConfig(this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { prefix = autoNumber.Prefix, mode = autoNumber.Mode });
                };
            Post["/SetupInvoiceAutoNumber"] = p =>
                {
                    try
                    {
                        this.InvoiceAutoNumberGenerator().SetupInvoiceAutoMumber(this.Request.Form.Mode, this.Request.Form.Prefix, this.CurrentAccount().OwnerId);
                        return Response.AsJson(new { error = false });
                    }
                    catch (Exception ex)
                    {
                        return Response.AsJson(new { error = true, message = ex.Message });
                    }
                };

            Post["/cancelinvoice/{id}"] = p =>
            {
                try
                {
                    this.InvoiceService().Cancel(p.id, this.Request.Form.invoice.note, this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { error = false });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e.Message });
                }
            };
        }
    }
}