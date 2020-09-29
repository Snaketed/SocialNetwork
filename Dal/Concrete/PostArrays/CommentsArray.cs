using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dal.Concrete.PostArrays
{
    public class CommentsArray : AArray<PostEntity, CommentEntity>
    {
        protected override string GetArrayFieldName()
        {
            return "Comments";
        }

        protected override IMongoCollection<BsonDocument> GetCollection()
        {
            return GetDatebase().GetCollection<BsonDocument>("posts");
        }
    }
}
