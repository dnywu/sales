using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.config;
namespace dokuku.sales.report
{
    public class EndpointConfig : NServiceBus.IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            NServiceBus.Configure.With()
                .DefaultBuilder()
                .BinarySerializer();
            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<MongoConfig>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singleton);
        }
    }
}