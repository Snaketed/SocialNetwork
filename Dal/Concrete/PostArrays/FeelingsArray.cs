using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dal.Concrete.PostArrays
{
    public class FeelingsArray : AArray<PostEntity, FeelingEntity>
    {
        protected override string GetArrayFieldName()
        {
            return "Feelings";
        }

        protected override IMongoCollection<BsonDocument> GetCollection()
        {
            return GetDatebase().GetCollection<BsonDocument>("posts");
        }
    }
}
