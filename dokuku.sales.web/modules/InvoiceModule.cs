using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Nancy.ViewEngines;
using System.IO;
using System.Dynamic;
using Nancy.ViewEngines.Razor;
using Nancy.Extensions;
using Antlr3.ST;
using dokuku.sales.invoices.viewtemplating;
using EO.Pdf;
using dokuku.sales.organization.model;
using System.Drawing;
using dokuku.sales.web.models;
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
                int countInvoice = this.InvoicesQueryRepository().CountInvoice(this.CurrentAccount().OwnerId);
                return Response.AsJson(countInvoice);
            };
            Get["/GetDataInvoiceToPaging/{start}/{limit}"] = p =>
              {
                  int start = p.start;
                  int limit = p.limit;
                  IEnumerable<Invoices>  invoices = this.InvoicesQueryRepository().GetDataInvoiceToPaging(this.CurrentAccount().OwnerId, start,limit);
                  return Response.AsJson(invoices);
              };
            Get["/GetDataInvoiceToPDF/{id}"] = p =>
            {
                Guid invoiceId = p.id;
                Invoices invoice = this.InvoicesQueryRepository().FindById(invoiceId, this.CurrentAccount().OwnerId);
                InvoiceReport invoiceReport = new InvoiceReport(invoice);
                Customer customer = this.CustomerReportRepository().GetCustomerById(Guid.Parse(invoice.CustomerId));
                Organization organization = this.OrganizationReportRepository().FindByOwnerId(this.CurrentAccount().OwnerId);

                LogoOrganization logo = this.LogoOrganizationQuery().GetLogo(this.CurrentAccount().OwnerId);                               
                DefaultTemplate template = new DefaultTemplate();

                string html = template.GetInvoiceDefaultTemplate(invoiceReport, customer, organization, logo);
                EO.Pdf.Runtime.AddLicense("aP0BELxbvNO/++OfmaQHEPGs4PP/6KFspbSzy653hI6xy59Zs7PyF+uo7sKe" +
                                        "tZ9Zl6TNGvGd3PbaGeWol+jyH+R2mbbA3a5rp7XDzZ+v3PYEFO6ntKbEzZ9o" +
                                        "tZGby59Zl8AEFOan2PgGHeR3q9bF266OzffU8MOSwdXjFvlww7vSIrx2s7ME" +
                                        "FOan2PgGHeR3hI7N2uui2un/HuR3hI514+30EO2s3MKetZ9Zl6TNF+ic3PIE" +
                                        "EMidtbjC4K9qq73K47J1pvD6DuSn6unaD71GgaSxy5914+30EO2s3OnP566l" +
                                        "4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Zl8DyD+NZ6w==");
                EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.3f, 7.5f, 10f);
                MemoryStream memStream = new MemoryStream();
                HtmlToPdf.ConvertHtml(html, memStream);
                MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
                return Response.FromStream(resultStream, "application/pdf");
            };
            Delete["/deleteInvoice/{invoiceId}"] = p =>
                {
                    try
                    {
                        Guid _id = Guid.Parse(p.invoiceId);
                        this.InvoiceService().Delete(_id, this.CurrentAccount().OwnerId);
                    }
                    catch (Exception ex)
                    {
                        return Response.AsJson(new { error = true, message = ex.Message });
                    }
                    return Response.AsJson("OK");
                };
            Get["/SearchInvoice/{key}"] = p =>
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
                    Guid invoiceId = p.id;
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
                        string Data = this.Request.Form.data;
                        InvoiceAutoNumberConfig config = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceAutoNumberConfig>(Data);
                        this.InvoiceAutoNumberGenerator().SetupInvoiceAutoMumber(config.Mode, config.Prefix, this.CurrentAccount().OwnerId);
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
                    this.InvoiceService().Cancel(p.id, this.Request.Form.Note, this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { error = false });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e.Message });
                }
            };

            Post["/forcecancelinvoice/{id}"] = p =>
            {
                try
                {
                    this.InvoiceService().ForceCancel(p.id, this.Request.Form.Note, this.CurrentAccount().OwnerId);
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