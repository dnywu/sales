using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.currency.messages
{
    public class CurrenciesUpdated : IMessage
    {
        public string CurrenciesUpdatedJson { get; set; }
    }
}
