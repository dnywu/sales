using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.item.model;
namespace dokuku.sales.item
{
    public class ItemCommand : IItemCommand
    {
        MongoCollection<BsonDocument> _document;
        MongoCollection<BsonDocument> _reports;
        public ItemCommand(MongoConfig mongoConfig)
        {
            _document = mongoConfig.MongoDatabase.GetCollection(typeof(Item).Name);
            _reports = mongoConfig.MongoDatabase.GetCollection(typeof(ItemReports).Name);
        }

        public void Save(Item item)
        {
            _document.Insert(item.ToBsonDocument());
            createIndex(item);
        }
        public void Update(Item item)
        {
            _document.Save(item.ToBsonDocument());
            updateIndex(item);
        }
        
        public void Delete(Guid id)
        {
            _document.Remove(Query.EQ("_id", id));
            deleteIndex(id);
        }
        private void createIndex(Item item)
        {
            _reports.Save<ItemReports>(new ItemReports(item));
            _reports.EnsureIndex(IndexKeys.Descending("Keywords"), IndexOptions.SetName("Keywords"));
        }
        private void updateIndex(Item item)
        {
            _reports.Save<ItemReports>(new ItemReports(item));
        }
        private void deleteIndex(Guid id)
        {
            _reports.Remove(Query.EQ("_id", id));
        }
    }
}