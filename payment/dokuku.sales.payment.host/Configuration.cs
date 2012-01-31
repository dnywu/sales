using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.NServiceBus;
using Ncqrs;
using Ncqrs.Config.StructureMap;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.MongoDB;
using MongoDB.Driver;
using NServiceBus;
namespace dokuku.sales.payment.host
{
    public static class Configuration
    {
        public static Configure InstallMongoDBEventStore(this Configure cfg)
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

            NcqrsEnvironment.SetDefault<IEventStore>(new MongoDBEventStore(mongo, SafeMode.True, "test"));
            return cfg;
        }
    }
}