using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.paymentmode.model;
using Nancy;
using Nancy.Security;
using dokuku.sales.invoices.model;

namespace dokuku.sales.web.modules
{
    public class PaymentModeModule : NancyModule
    {
        public PaymentModeModule()
        {
            this.RequiresAuthentication();

            Post["/createpaymentmode"] = p =>
            {
                try
                {
                    var paymentmode = this.Request.Form.paymentmode;
                    PaymentModes paymentMode = this.PaymentModeService().Insert(paymentmode, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentMode);                    
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Get["/findpaymentmodebyid/{id}"] = p =>
            {
                try
                {
                    PaymentModes paymentMode = this.PaymentModeQuery().Get(p.id, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentMode);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Get["/findallpaymentmode"] = p =>
            {
                try
                {
                    PaymentModes[] paymentModes = this.PaymentModeQuery().FindAll(this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentModes);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Post["/updatepaymentmode"] = p =>
            {
                try
                {
                    PaymentModes paymentMode = this.PaymentModeService().Update(this.Request.Form.paymentmode, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentMode);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };
            Post["/UpdateStatusInvoice/{id}"] = p =>
            {
                try
                {
                    Guid invoiceId = p.id;
                    Invoices invoice = this.InvoicesQueryRepository().FindById(invoiceId, this.CurrentAccount().OwnerId);
                    invoice.InvoiceStatusSudahLunas();
                   
                    this.InvoiceService().Update(this.Request.Form.invoice, this.CurrentAccount().OwnerId);
                    return Response.AsJson(new { error = false });                
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Post["/deletepaymentmode/{id}"] = p =>
            {
                try
                {
                    this.PaymentModeService().Delete(p.id);
                    return Response.AsJson(new { error = false });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };
        }
    }
}