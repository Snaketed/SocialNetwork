using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dal.Entity.Abstract
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
    }
}
