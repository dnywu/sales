using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.invoices.host
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .BinarySerializer()
                .InstallNcqrs()
                .MsmqTransport()
                .PurgeOnStartup(true);
        }
    }
}