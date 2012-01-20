using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.customer.model;
using dokuku.sales.customer.repository;
using NServiceBus;
using dokuku.sales.customer.messages;
using MongoDB.Bson;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.customer.Service
{
    public class CustomerService : ICustomerService
    {
        MongoConfig mongo;
        IBus bus;
        public CustomerService(MongoConfig mongo, IBus bus)
        {
            this.mongo = mongo;
            this.bus = bus;
        }
        public string SaveCustomer(string customerJson, string ownerId)
        {
            Customer cs = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(customerJson);
            cs._id = Guid.NewGuid();
            cs.OwnerId = ownerId;
            
            Collections.Save<Customer>(cs);
            var result = cs.ToJson<Customer>();
            if (bus != null)
                bus.Publish<CustomerCreated>(new CustomerCreated { Data = result });
            return result;
        }

        public void UpdateCustomer(string customerJson)
        {
            Customer cs = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(customerJson);
            Collections.Save<Customer>(cs);

            if (bus != null)
            bus.Publish<CustomerUpdated>(new CustomerUpdated { Data = cs.ToJson() });
        }

        public void DeleteCustomer(Guid id)
        {
            Collections.Remove(Query.EQ("_id", BsonValue.Create(id)));
            if (bus != null)
            bus.Publish<CustomerDeleted>(new CustomerDeleted { Id = id });
        }
        private MongoCollection Collections
        {
            get
            {
                return mongo.MongoDatabase.GetCollection(typeof(Customer).Name);
            }
        }
    }
}