using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceDeleted
    {
        public Guid InvoiceId { get; set; }
        public string UserName { get; set; }
    }
}
