using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.model;
using MongoDB.Driver;
using MongoDB.Bson;
using dokuku.sales.config;
using MongoDB.Driver.Builders;
using NServiceBus;
using dokuku.sales.taxes.messages;

namespace dokuku.sales.taxes.service
{
    public class ServiceTax : IServiceTax
    {
        MongoCollection<BsonDocument> _collections;
        IBus _bus;
        public ServiceTax(MongoConfig mongo, IBus bus)
        {
            _collections = mongo.MongoDatabase.GetCollection(typeof(Taxes).Name);
            _bus = bus;
        }
        public Taxes Create(string taxJson,string ownerId)
        {
            Taxes tax = Newtonsoft.Json.JsonConvert.DeserializeObject<Taxes>(taxJson);
            tax.OwnerId = ownerId;
            tax._id = Guid.NewGuid();
            _collections.Save(tax);

            if (_bus != null)
                _bus.Publish(new TaxCreated { TaxJson = tax.ToJson() });
            return tax;
        }
        public void Update(string taxJson,string ownerId)
        {
            Taxes tax = Newtonsoft.Json.JsonConvert.DeserializeObject<Taxes>(taxJson);
            tax.OwnerId = ownerId;
            _collections.Save(tax);
            if( _bus != null)
            _bus.Publish(new TaxUpdated { TaxUpdatedJson = tax.ToJson() });
        }
        public void Delete(Guid guid)
        {
            _collections.Remove(Query.EQ("_id", guid));
            if (_bus != null)
                _bus.Publish(new TaxDeleted { Id = guid });
        }
    }
}
