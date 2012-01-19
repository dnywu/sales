using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
namespace dokuku.sales.report
{
    public class EndpointConfig : NServiceBus.IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            NServiceBus.Configure.With()
                .DefaultBuilder()
                .BinarySerializer();
        }
    }
}