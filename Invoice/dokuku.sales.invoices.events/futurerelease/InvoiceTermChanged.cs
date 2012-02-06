using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
using dokuku.sales.invoices.common;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceTermChanged
    {
        public Guid InvoiceId { get; set; }
        public Term Term { get; set; }
        public DateTime DueDate { get; set; }
        public string UserName { get; set; }
    }
}
