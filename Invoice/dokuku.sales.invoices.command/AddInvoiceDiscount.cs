using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace dokuku.sales.invoices.commands
{
    [Serializable]
    public class AddInvoiceDiscount : CommandBase
    {
        public Guid InvoiceId { get; set; }
        public decimal DiscountInPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
    }
}
