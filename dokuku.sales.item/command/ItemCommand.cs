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
        MongoConfig mongo;
        public ItemCommand(MongoConfig mongoConfig)
        {
            mongo = mongoConfig;
        }

        public void Save(Item item)
        {
            Collections.Save<Item>(item);
        }

        public void Delete(Guid id)
        {
            Collections.Remove(Query.EQ("_id", id));
        }

        public MongoCollection<Item> Collections
        {
            get { return mongo.MongoDatabase.GetCollection<Item>(typeof(Item).Name); }
        }
    }
}