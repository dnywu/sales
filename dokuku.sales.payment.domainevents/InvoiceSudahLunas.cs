using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.domainevents
{
    public class InvoiceSudahLunas : IDomainEvent
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string ownerid { get; set; }
    }
}