using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.item.messages
{
    [Serializable]
    public class ItemCreated : IMessage
    {
        public string Data { get; set; }
    }
}