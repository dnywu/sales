using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoice.messages;
using NServiceBus;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;

namespace dokuku.sales.invoices.service
{
    public class InvoiceApproveHandler : IMessageHandler<InvoiceApproved>
    {
        IInvoiceService InvoiceService { get; set; }
        public void Handle(InvoiceApproved message)
        {
            InvoiceService.UpdateStatusToAprrove(message.InvoiceId, message.OwnerId);
        }
    }
}