using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    public class PriceInvoiceItemChanged
    {
        public Guid InvoiceId { get; set; }
        public Guid InvoiceItemId { get; set; }
        public decimal InvoiceItemPrice { get; set; }
        public decimal InvoiceItemTotal { get; set; }
        public string UserName { get; set; }
    }
}
