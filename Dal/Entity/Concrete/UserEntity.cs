using Dal.Entity.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Dal.Entity.Concrete
{
    [BsonIgnoreExtraElements]
    public class UserEntity : IEntity
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string Nickname { get; set; }
        [BsonIgnoreIfNull]
        public string Password { get; set; }

        [BsonIgnoreIfNull]
        public string PhoneNumber { get; set; }
        [BsonIgnoreIfNull]
        public string MailAddress { get; set; }

        [BsonIgnoreIfNull]
        public string FirstName { get; set; }
        [BsonIgnoreIfNull]
        public string LastName { get; set; }
        [BsonIgnoreIfNull]
        public string BornDate { get; set; }

        [BsonIgnoreIfNull]
        public IList<ObjectId> Subscribers { get; set; }
        [BsonIgnoreIfNull]
        public IList<ObjectId> FavoriteUsers { get; set; }
    }
}
