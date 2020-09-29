using Dal.Entity.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Dal.Entity.Concrete.PostArrayEntities
{
    public class CommentEntity : IEntity
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfNull]
        public string Text { get; set; }
        [BsonIgnoreIfNull]
        public string Date { get; set; }
        [BsonIgnoreIfDefault]
        public ObjectId CommentOwnerId { get; set; }
    }
}
