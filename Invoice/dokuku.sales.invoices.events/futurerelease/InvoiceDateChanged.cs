using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceDateChanged
    {
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public Guid InvoiceId { get; set; }
        public string UserName { get; set; }
    }
}
