using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.currency.messages
{
    public class CurrencyCreated : IMessage
    {
        public string CurrenciesCreatedJson { get; set; }
    }
}
