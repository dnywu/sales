using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.taxes.messages
{
    public class TaxUpdated : IMessage
    {
        public string TaxUpdatedJson { get; set; }
    }
}
