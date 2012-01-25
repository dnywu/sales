using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.config;
using dokuku.sales.domainevents;
using dokuku.sales.payment.service;

namespace dokuku.sales.payment.host
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

            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<InvoiceFullPaidHandler>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);
            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<InvoicePartiallyPaidHandler>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);
            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<PaymentInvoiceRecordedHandler>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);
            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<PaymentRecordRevisedHandler>(
                                    NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);

            DomainEvents.Container = new ContainerDomainEvents() { ObjectBuilder = NServiceBus.Configure.Instance.Builder };
        }
    }
}