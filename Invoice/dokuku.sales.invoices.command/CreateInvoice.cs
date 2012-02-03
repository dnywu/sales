using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    public class CreateInvoice : CommandBase
    {
        public Guid InvoiceId { get; set; }
        public Guid CustomerId { get; set; }
        public string PONumber { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
    }
}
