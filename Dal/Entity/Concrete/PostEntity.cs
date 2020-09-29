using Dal.Entity.Abstract;
using Dal.Entity.Concrete.PostArrayEntities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Dal.Entity.Concrete
{
    [BsonIgnoreExtraElements]
    public class PostEntity : IEntity
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfNull]
        public string Text { get; set; }
        [BsonIgnoreIfNull]
        public string PicturePath { get; set; }
        [BsonIgnoreIfNull]
        public string Date { get; set; }
        [BsonIgnoreIfDefault]
        public ObjectId PostOwnerId { get; set; }
        [BsonIgnoreIfNull]
        public IList<FeelingEntity> Feelings { get; set; }
        [BsonIgnoreIfNull]
        public IList<CommentEntity> Comments { get; set; }
    }
}
