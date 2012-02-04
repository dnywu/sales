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
using dokuku.sales.payment.common;
namespace dokuku.sales.web.modules
{
    public class PaymentReceivedModule : Nancy.NancyModule
    {
        public PaymentReceivedModule()
        {
            this.RequiresAuthentication();
            Post["/UpdatePaymentReceived"] = p =>
            {
                try
                {
                    //var paymentReceived = this.Request.Form.dataPaymentReceived;
                    //InvoicePayment dataPayment = JsonConvert.DeserializeObject<InvoicePayment>(paymentReceived);
                    //PayInvoice cmd = new PayInvoice
                    //{
                    //    InvoiceId = dataPayment.InvoiceId,
                    //     PaymentId= Guid.NewGuid(),
                    //    AmountPaid = dataPayment.AmountReceived,
                    //    BankCharge = dataPayment.BankChanges,
                    //    Notes = dataPayment.Notes,
                    //    PaymentDate = dataPayment.Date,
                    //    PaymentMode = dataPayment.PaymentMethod,
                    //    Reference = dataPayment.Reference   
                    //};

                    //this.Bus().Send("dokukuPaymentDistributorDataBus", new CommandMessage{Payload = cmd});
                    return Response.AsJson(new { error = false, message = "OK" });
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };

            Get["/GetAllPaymentReceived"] = p =>
            {
                try
                {
                    //var paymentReceived = this.Request.Form.dataPaymentReceived;
                    //InvoicePayment dataPayment = JsonConvert.DeserializeObject<InvoicePayment>(paymentReceived);
                    IList<PayInvoice> listpayment = new List<PayInvoice>();
                    PayInvoice cmd = new PayInvoice
                    {
                        InvoiceId = Guid.NewGuid(),
                        PaymentId = Guid.NewGuid(),
                        AmountPaid = 9900,
                        BankCharge = 1000,
                        Notes = "belum lunas",
                        PaymentDate = DateTime.Now,
                        PaymentMode = new PaymentMode() { Code="001", Id=Guid.NewGuid(), Name="Transfer Bank" },
                        Reference = "sudah dibayar"
                    };
                    PayInvoice cmd1 = new PayInvoice
                    {
                        InvoiceId = Guid.NewGuid(),
                        PaymentId = Guid.NewGuid(),
                        AmountPaid = 10000,
                        BankCharge = 1000,
                        Notes = "sudah lunas",
                        PaymentDate = DateTime.Now,
                        PaymentMode = new PaymentMode() { Code = "001", Id = Guid.NewGuid(), Name = "Transfer Bank" },
                        Reference = "sudah dibayar"
                    };

                    listpayment.Add(cmd);
                    listpayment.Add(cmd1);

                    //this.Bus().Send("dokukuPaymentDistributorDataBus", new CommandMessage { Payload = cmd });
                    return Response.AsJson(cmd);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/GetPaymentReceivedById/{id}"] = p =>
            {
                try
                {
                     var paymentId = p.id.ToString();
                    //InvoicePayment dataPayment = JsonConvert.DeserializeObject<InvoicePayment>(paymentReceived);
                    //PayInvoice cmd = new PayInvoice
                    //{
                    //    InvoiceId = dataPayment.InvoiceId,
                    //    PaymentId = Guid.NewGuid(),
                    //    AmountPaid = dataPayment.AmountReceived,
                    //    BankCharge = dataPayment.BankChanges,
                    //    Notes = dataPayment.Notes,
                    //    PaymentDate = dataPayment.Date,
                    //    PaymentMode = dataPayment.PaymentMethod,
                    //    Reference = dataPayment.Reference
                    //};

                    //this.Bus().Send("dokukuPaymentDistributorDataBus", new CommandMessage { Payload = cmd });
                    return Response.AsJson(new { error = false, message = "OK" });
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };

            Get["/GetDataPaymentReceivedToPaging/{start}/{limit}"] = p =>
            {
                //int start=0;
                //int limit=0; 
                //IEnumerable<Invoices> invoices = this.InvoicesQueryRepository().GetDataInvoiceToPaging(this.CurrentAccount().OwnerId, start, limit);
                return Response.AsJson("");
            };
           
        }
    }
}