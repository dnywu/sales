using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
namespace dokuku.sales.invoices.events
{
    [Serializable]
    public class InvoiceExchangeRateChanged
    {
        public Guid InvoiceId { get; set; }
        public decimal ExchangeRate { get; set; }
        public string UserName { get; set; }
    }
}
