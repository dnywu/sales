using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.common;
namespace dokuku.sales.invoices.commands
{
    [Serializable]
    public class AddInvoiceItem : CommandBase
    {
        public Guid InvoiceId { get; set; }
        public InvoiceItem Item { get; set; }
        public string UserName { get; set; }
    }
}
