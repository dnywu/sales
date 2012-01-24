using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.messages;
using NServiceBus;
using dokuku.sales.config;
using dokuku.sales.invoices.model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.invoices.query;
using dokuku.sales.invoices.command;
namespace dokuku.sales.invoices.service
{
    public class FullPaymentInvoiceHandler : IHandleMessages<InvoiceFullPaid>
    {
        IInvoiceService InvoiceService { get; set; }
        public void Handle(InvoiceFullPaid message)
        {
            InvoiceService.InvoiceFullyPaid(message.InvoiceId, message.ownerid);
        }
    }
}