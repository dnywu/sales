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
    public class InvoicePaymentModule : Nancy.NancyModule
    {
        public InvoicePaymentModule()
        {
            this.RequiresAuthentication();
            Post["/pay"] = p =>
            {
                try
                {
                    var invoicePayment = this.Request.Form.invoicepayment;
                    object result = null;
                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Post["/SendToEmail/{emailTo}"] = p =>
            {
                try
                {
                    var emailTo = p.emailTo.ToString();
                    object result = null;
                    return Response.AsJson(result);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };          
        }
    }
}