using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace dokuku.sales.config
{
    public class CouchDBConfig : ConfigurationSection
    {
        public CouchDBConfig()
        {
        }
        [ConfigurationProperty("server",
                               DefaultValue = "tcloud1.bonastoco.com",
                               IsRequired = true)]
        public String Server
        {
            get
            { return (String)this["server"]; }
            set
            { this["server"] = value; }
        }

        [ConfigurationProperty("database",
                               DefaultValue = "dokuku",
                               IsRequired = true)]
        public string Database
        {
            get
            {
                return (String)this["database"];
            }
            set
            {
                this["database"] = value;
            }
        }
        
        [ConfigurationProperty("port", DefaultValue=5984, IsRequired = true)]
        public int Port
        {
            get
            { return (int)this["port"]; }
            set
            { this["port"] = value; }
        }
        [ConfigurationProperty("username", IsRequired = true)]
        public String Username
        {
            get
            { return (String)this["username"]; }
            set
            { this["username"] = value; }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public String Password
        {
            get
            { return (String)this["password"]; }
            set
            { this["password"] = value; }
        }
    }
}