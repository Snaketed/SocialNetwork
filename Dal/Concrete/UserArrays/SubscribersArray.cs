using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dal.Concrete.UserArrays
{
    public class SubscribersArray : AArray<UserEntity, ObjectId>
    {
        protected override string GetArrayFieldName()
        {
            return "Subscribers";
        }

        protected override IMongoCollection<BsonDocument> GetCollection()
        {
            return GetDatebase().GetCollection<BsonDocument>("users");
        }
    }
}
