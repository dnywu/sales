using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Configuration;

namespace dokuku.sales.config
{
    public class MongoConfig
    {
        static readonly MongoConfig instance = Nested.instance;

        private MongoConfig()
        {
            MongoConfigSection mongoSection = (MongoConfigSection)ConfigurationManager.GetSection(MongoConfigSection.DefaultSectionName);

            MongoServerSettings settings = new MongoServerSettings();
            settings.ConnectionMode = mongoSection.ConnectionMode;
            settings.ReplicaSetName = mongoSection.ReplicaSetName;

            if (mongoSection.ServerAddresses.Count == 0)
                throw new ConfigurationException("No server has been define in configuration");

            var servers = new List<MongoServerAddress>();
            foreach (ServerAddress serverAddr in mongoSection.ServerAddresses)
            {
                servers.Add(new MongoServerAddress(serverAddr.Server, serverAddr.Port));
            }
            settings.Servers = servers;
            MongoServer = MongoServer.Create(settings);
            MongoDatabase = MongoServer.GetDatabase(mongoSection.Database, new MongoCredentials(mongoSection.UserName, mongoSection.Password, mongoSection.MongoAdmin));
        }
        public MongoServer MongoServer { get; private set; }
        public MongoDatabase MongoDatabase { get; private set; }

        public static MongoConfig Instance { get { return instance; } }

        class Nested
        {
            static Nested() { }
            internal static readonly MongoConfig instance = new MongoConfig();
        }
    }
}
