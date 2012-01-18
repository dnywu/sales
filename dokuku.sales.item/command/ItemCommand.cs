using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.item
{
    public class ItemCommand : IItemCommand
    {
        MongoCollection<BsonDocument> _document;
        public ItemCommand(MongoConfig mongoConfig)
        {
            _document = mongoConfig.MongoDatabase.GetCollection(typeof(Item).Name);
        }

        public void Save(Item item)
        {
            _document.Insert(item.ToBsonDocument());
            _document.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        public void Update(Item item)
        {
            _document.Save(item.ToBsonDocument());
        }
        
        public void Delete(Guid id)
        {
            _document.Remove(Query.EQ("_id", id));
        }
    }
}