using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.messages;
using NServiceBus;
namespace dokuku.sales.invoices.service
{
    public class PartiallyPaymentInvoiceHandler : IHandleMessages<InvoicePartiallyPaid>
    {
        public IInvoiceService InvoiceService { get; set; }
        public void Handle(InvoicePartiallyPaid message)
        {
            InvoiceService.InvoicePartialyPaid(message.InvoiceId, message.ownerid);
        }
    }
}
