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
    [MapsToAggregateRootConstructor("dokuku.sales.invoices.domain.Invoice, dokuku.sales.invoices.domain")]
    public class CreateInvoice : CommandBase
    {
        public Guid CustomerId { get; set; }
        public string PONo { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
    }
}
