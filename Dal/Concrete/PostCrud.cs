using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using MongoDB.Driver;

namespace Dal.Concrete
{
    public class PostCrud : ACrud<PostEntity>
    {
        protected override IMongoCollection<PostEntity> GetCollection()
        {
            return GetDatebase().GetCollection<PostEntity>("posts");
        }
    }
}
