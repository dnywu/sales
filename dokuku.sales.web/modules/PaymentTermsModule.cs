using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.paymentterms.model;
using Nancy;
using Nancy.Security;
using dokuku.sales.invoices.model;

namespace dokuku.sales.web.modules
{
    public class PaymentTermsModule : NancyModule
    {
        public PaymentTermsModule()
        {
            this.RequiresAuthentication();

            Post["/createpaymentterms"] = p =>
            {
                try
                {
                    var paymentterms = this.Request.Form.paymentterms;
                    PaymentTerms paymentTerms = this.PaymentTermsService().Insert(paymentterms, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentTerms);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Get["/findpaymenttermsbyid/{id}"] = p =>
            {
                try
                {
                    PaymentTerms paymentterms = this.PaymentTermsQuery().Get(p.id, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentterms);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Get["/findallpaymentterms"] = p =>
            {
                try
                {
                    PaymentTerms[] paymentterms = this.PaymentTermsQuery().FindAll(this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentterms);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };

            Post["/updatepaymentterms"] = p =>
            {
                try
                {
                    PaymentTerms paymentterms = this.PaymentTermsService().Update(this.Request.Form.paymentterms, this.CurrentAccount().OwnerId);
                    return Response.AsJson(paymentterms);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { error = true, message = e });
                }
            };
          
            Post["/deletepaymentterms/{id}"] = p =>
            {
                try
                {
                    this.PaymentTermsService().Delete(p.id);
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