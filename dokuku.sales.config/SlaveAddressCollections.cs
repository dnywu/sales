using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace dokuku.sales.config
{
    public class SlaveAddressCollections : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SlaveAddress();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            SlaveAddress serverAddr = (SlaveAddress)element;
            return String.Concat(serverAddr.Server, ":", serverAddr.Port);
        }
    }
}
