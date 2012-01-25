using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.domainevents;
using dokuku.sales.payment.domainevents;
using NServiceBus;
using dokuku.sales.payment.messages;
namespace dokuku.sales.payment.service
{
    public class InvoiceFullPaidHandler : Handles<InvoiceSudahLunas>
    {
        public IBus Bus { get; set; }
        public void Handle(InvoiceSudahLunas args)
        {
            Bus.Publish(new InvoiceFullPaid
            {
                InvoiceId = args.InvoiceId,
                InvoiceNumber = args.InvoiceNumber,
                ownerid = args.ownerid
            });
        }
    }
}