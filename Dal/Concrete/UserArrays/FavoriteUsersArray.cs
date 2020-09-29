using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dal.Concrete.UserArrays
{
    public class FavoriteUsersArray : AArray<UserEntity, ObjectId>
    {
        protected override string GetArrayFieldName()
        {
            return "FavoriteUsers";
        }

        protected override IMongoCollection<BsonDocument> GetCollection()
        {
            return GetDatebase().GetCollection<BsonDocument>("users");
        }
    }
}
