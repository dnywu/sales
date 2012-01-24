using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace dokuku.sales.config
{
    public class ServerAddressCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerAddress();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            ServerAddress serverAddr = (ServerAddress)element;
            return String.Concat(serverAddr.Server, ":", serverAddr.Port);
        }
    }
}