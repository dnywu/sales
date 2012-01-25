using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.model;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.taxes.query
{
    public class TaxQueryRepository : ITaxQueryRepository
    {
        MongoCollection<BsonDocument> _collections;
        public TaxQueryRepository(MongoConfig mongo)
        {
            _collections = mongo.ReportingDatabase.GetCollection(typeof(Taxes).Name);
        }
        public Taxes GetTaxById(Guid guid,string ownerId)
        {
            return _collections.FindOneAs<Taxes>(Query.And(Query.EQ("_id", guid), Query.EQ("OwnerId", ownerId)));
        }
        public IEnumerable<Taxes> GetAllTaxes(string OwnerId)
        {
            return _collections.FindAs<Taxes>(Query.EQ("OwnerId", OwnerId));
        }
    }
}