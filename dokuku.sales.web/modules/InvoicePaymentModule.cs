using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using dokuku.sales.payment.commands;
using dokuku.sales.payment.readmodel;
using StructureMap;
using NServiceBus;
using Ncqrs.NServiceBus;
using dokuku.sales.web.models;
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
                    var invoicepayment = this.Request.Form.invoicepayment;
                    InvoicePayment dataPayment = JsonConvert.DeserializeObject<InvoicePayment>(invoicepayment);
                    PayInvoice cmd = new PayInvoice
                    {
                        InvoiceId = dataPayment.InvoiceId,
                         PaymentId= Guid.NewGuid(),
                        AmountPaid = dataPayment.AmountReceived,
                        BankCharge = dataPayment.BankChanges,
                        Notes = dataPayment.Notes,
                        PaymentDate = dataPayment.Date,
                        PaymentMode = dataPayment.PaymentMethod,
                        Reference = dataPayment.Reference   
                    };

                    this.Bus().Send("dokukuPaymentDistributorDataBus", new CommandMessage{Payload = cmd});
                    return Response.AsJson(new { error = false, message = "OK" });
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