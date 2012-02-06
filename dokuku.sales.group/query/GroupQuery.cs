using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using dokuku.sales.group.model;

namespace dokuku.sales.group.query
{
    public class GroupQuery : IGroupQuery
    {
        MongoCollection<BsonDocument> _collections;
        public GroupQuery(MongoConfig config)
        {
            _collections = config.MongoDatabase.GetCollection(typeof(Group).Name);
        }
        public Group Get(Guid id, string ownerId)
        {
            return _collections.FindOneAs<Group>(Query.EQ("_id", id));
        }
        public Group FindByName(string name, string ownerId)
        {
            return _collections.FindOneAs<Group>(Query.And(Query.EQ("Name", name), Query.EQ("OwnerId", ownerId)));
        }
        public Group FindByNameAndId(string name, Guid id, string ownerId)
        {
            return _collections.FindOneAs<Group>(Query.And(Query.EQ("Name", name), Query.EQ("_id", id), Query.EQ("OwnerId", ownerId)));
        }
        public Group[] FindAll(string ownerId)
        {
            return _collections.FindAs<Group>(Query.EQ("OwnerId", ownerId)).ToArray();
        }
    }
}