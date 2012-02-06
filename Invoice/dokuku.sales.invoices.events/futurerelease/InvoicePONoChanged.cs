using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoicePONoChanged
    {
        public Guid InvoiceId { get; set; }
        public string PONo { get; set; }
        public string UserName { get; set; }
    }
}
