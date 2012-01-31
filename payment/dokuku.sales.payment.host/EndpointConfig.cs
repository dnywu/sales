using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using Ncqrs;
using Ncqrs.Config.StructureMap;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.MongoDB;
using MongoDB.Driver;
namespace dokuku.sales.payment.host
{
    public class EndpointConfig : NServiceBus.IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization,  IWantToRunAtStartup
    {
        public void Init()
        {
            NServiceBus.Configure.With()
               .DefaultBuilder()
               .BinarySerializer()
               .MsmqSubscriptionStorage();
        }

        public void Run()
        {
            NServiceBus.Configure.Instance.InstallNcqrs();
            NServiceBus.Configure.Instance.InstallMongoDBEventStore();
        }

        public void Stop()
        {
        }
    }
}