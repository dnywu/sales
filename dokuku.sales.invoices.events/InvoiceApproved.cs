using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceApproved
    {
        public Guid _id { get; set; }
        public string InvoiceNo { get; set; }
        public string OwnerId { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}
