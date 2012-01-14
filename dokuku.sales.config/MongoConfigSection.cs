using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MongoDB.Driver;

namespace dokuku.sales.config
{
    public class MongoConfigSection : ConfigurationSection
    {
        public const string DefaultSectionName = "mongo.config";

        [ConfigurationProperty("replicaSetName")]
        public string ReplicaSetName
        {
            get { return (string)this["replicaSetName"]; }
            set { this["replicaSetName"] = value; }
        }
        [ConfigurationProperty("connectionMode")]
        public ConnectionMode ConnectionMode
        {
            get { return (ConnectionMode)this["connectionMode"]; }
            set { this["connectionMode"] = value; }
        }
        [ConfigurationProperty("slaveOk")]
        public bool SlaveOk
        {
            get { return (bool)this["slaveOk"]; }
            set { this["slaveOk"] = value; }
        }
        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get { return (string)this["database"]; }
            set { this["database"] = value; }
        }
        [ConfigurationProperty("username")]
        public string UserName
        {
            get { return (string)this["username"]; }
            set { this["username"] = value; }
        }
        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
        [ConfigurationProperty("admin")]
        public bool MongoAdmin
        {
            get { return (bool)this["admin"]; }
            set { this["admin"] = value; }
        }
        [ConfigurationCollection(typeof(ServerAddressCollection))]
        [ConfigurationProperty("serverAddresses")]
        public ServerAddressCollection ServerAddresses
        {
            get { return (ServerAddressCollection)this["serverAddresses"]; }
            set { this["serverAddresses"] = value; }
        }
        [ConfigurationCollection(typeof(SlaveAddressCollections))]
        [ConfigurationProperty("slaveAddresses")]
        public ServerAddressCollection SlaveAddresses
        {
            get { return (ServerAddressCollection)this["slaveAddresses"]; }
            set { this["slaveAddresses"] = value; }
        }
    }
}
