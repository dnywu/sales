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
            MongoServerSettings reportingSettings = new MongoServerSettings();
            settings.ConnectionMode = mongoSection.ConnectionMode;
            settings.ReplicaSetName = mongoSection.ReplicaSetName;
            reportingSettings.ConnectionMode = mongoSection.ConnectionMode;
            reportingSettings.ReplicaSetName = mongoSection.ReplicaSetName;
            settings.SlaveOk = mongoSection.SlaveOk;
            reportingSettings.SlaveOk = mongoSection.SlaveOk;

            if (mongoSection.ServerAddresses.Count == 0)
                throw new ConfigurationErrorsException("No server has been define in configuration");
            if (mongoSection.SlaveAddresses.Count == 0)
                throw new ConfigurationErrorsException("No server has been define in configuration");
            var servers = new List<MongoServerAddress>();
            foreach (ServerAddress serverAddr in mongoSection.ServerAddresses)
            {
                servers.Add(new MongoServerAddress(serverAddr.Server, serverAddr.Port));
            }
            settings.Servers = servers;
            var reportingServers = new List<MongoServerAddress>();
            foreach (SlaveAddress slaveAddr in mongoSection.SlaveAddresses)
            {
                reportingServers.Add(new MongoServerAddress(slaveAddr.Server, slaveAddr.Port));
            }
            reportingSettings.Servers = reportingServers;

            MongoServer = MongoServer.Create(settings);
            MongoDatabase = MongoServer.GetDatabase(mongoSection.Database, new MongoCredentials(mongoSection.UserName, mongoSection.Password, mongoSection.MongoAdmin));
            ReportingServer = MongoServer.Create(reportingSettings);
            MongoDatabase = ReportingServer.GetDatabase(mongoSection.Database, new MongoCredentials(mongoSection.UserName, mongoSection.Password, mongoSection.MongoAdmin));
        }
        public MongoServer MongoServer { get; private set; }
        public MongoServer ReportingServer { get; private set; }
        public MongoDatabase MongoDatabase { get; private set; }
        public MongoDatabase ReportingDatabase { get; private set; }
        public static MongoConfig Instance { get { return instance; } }

        class Nested
        {
            static Nested() { }
            internal static readonly MongoConfig instance = new MongoConfig();
        }
    }
}