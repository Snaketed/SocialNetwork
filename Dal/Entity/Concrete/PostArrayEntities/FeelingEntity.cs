using Dal.Entity.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Dal.Entity.Concrete.PostArrayEntities
{
    public class FeelingEntity : IEntity
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfNull]
        public bool Like { get; set; }
        [BsonIgnoreIfDefault]
        public ObjectId FeelerId { get; set; }
        [BsonIgnoreIfNull]
        public string Date { get; set; }
    }
}
