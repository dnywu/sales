using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using dokuku.sales.group.model;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace dokuku.sales.group.service
{
    public class GroupService : IGroupService
    {
        MongoCollection<BsonDocument> collections;
        public GroupService(MongoConfig mongo)
        {
            collections = mongo.MongoDatabase.GetCollection(typeof(Group).Name);
        }
        public Group Insert(string json, string ownerId)
        {
            Group group = JsonConvert.DeserializeObject<Group>(json);
            group._id = Guid.NewGuid();
            group.OwnerId = ownerId;
            collections.Save(group);
            return group;
        }
        public Group Update(string json,string ownerId)
        {
            Group group = JsonConvert.DeserializeObject<Group>(json);
            group.OwnerId = ownerId;
            collections.Save(group);
            return group;
        }
        public void Delete(Guid id)
        {
            collections.Remove(Query.EQ("_id", id));
        }
    }
}