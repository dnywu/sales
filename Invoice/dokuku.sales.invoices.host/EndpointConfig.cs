using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.invoices.autonumbergenerator;
using MongoDB.Driver;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.MongoDB;
using Ncqrs;
using dokuku.sales.config;
using Ncqrs.Commanding.ServiceModel;
using dokuku.sales.invoices.commands;
namespace dokuku.sales.invoices.host
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, IWantToRunAtStartup
    {
        static MongoConfig mongo;
        
        public void Init()
        {
            NServiceBus.Configure.With()
              .DefaultBuilder()
              .BinarySerializer()
              .MsmqSubscriptionStorage();

            mongo = new MongoConfig();
            NServiceBus.Configure.Instance.Configurer.RegisterSingleton<MongoConfig>(mongo);
            NServiceBus.Configure.Instance.Configurer.ConfigureComponent<InvoiceAutoNumberGenerator>(NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);
        }

        public void InstallMongoDBEventStore()
        {
            MongoServerSettings settings = new MongoServerSettings();
            settings.ConnectionMode = ConnectionMode.ReplicaSet;
            settings.ReplicaSetName = "dokukuSet";
            settings.DefaultCredentials = new MongoCredentials("admin", "S31panas", true);
            settings.SlaveOk = true;
            settings.Servers = new List<MongoServerAddress>
            {
                new MongoServerAddress("tcloud2.bonastoco.com", 27021),
                new MongoServerAddress("tcloud2.bonastoco.com", 27022)
            };
            MongoServer mongo = MongoServer.Create(settings);

            NcqrsEnvironment.SetDefault<IEventStore>(new MongoDBEventStore(mongo, SafeMode.True, "Invoice"));
           // NcqrsEnvironment.Get<CommandService>().RegisterExecutor<Ncqrs.Commanding.CommandExecution.ICommandExecutor<CreateInvoice>>(

        }


        public void Run()
        {
            NServiceBus.Configure.Instance.InstallNcqrs();
            InstallMongoDBEventStore();
        }

        public void Stop()
        {
        }
    }
}