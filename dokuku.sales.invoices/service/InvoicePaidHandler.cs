using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.config;
using dokuku.sales.invoices.model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.invoices.query;
using dokuku.sales.invoices.command;
using dokuku.sales.payment.events;
namespace dokuku.sales.invoices.service
{
    public class InvoicePaidHandler : IHandleMessages<InvoicePaid>
    {
        IInvoiceService InvoiceService { get; set; }
        public void Handle(InvoicePaid message)
        {
            //InvoiceService.InvoiceFullyPaid(message.InvoiceId, message.ownerid);
        }
    }
}