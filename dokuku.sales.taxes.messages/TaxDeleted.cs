using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.taxes.messages
{
    public class TaxDeleted : IMessage
    {
        public Guid Id { get; set; }
    }
}