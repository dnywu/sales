using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class DescriptionInvoiceItemChanged
    {
        public Guid InvoiceId { get; set; }
        public Guid InvoiceItemId { get; set; }
        public string InvoiceitemDescription { get; set; }
        public string UserName { get; set; }
    }
}
