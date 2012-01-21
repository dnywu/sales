using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.config;

namespace dokuku.sales.payment.service
{
    public class EndpointConfig : NServiceBus.IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            NServiceBus.Configure.With()
                .DefaultBuilder()
                .BinarySerializer()
                .MsmqSubscriptionStorage();

            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<MongoConfig>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singleton);
        }
    }
}