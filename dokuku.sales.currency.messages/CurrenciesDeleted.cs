using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.currency.messages
{
    public class CurrenciesDeleted : IMessage
    {
        public Guid Id { get; set; }
    }
}
