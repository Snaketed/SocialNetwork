using Dal.Abstract.AbstractClasses;
using Dal.Entity.Concrete;
using MongoDB.Driver;

namespace Dal.Concrete
{
    public class UserCrud : ACrud<UserEntity>
    {
        protected override IMongoCollection<UserEntity> GetCollection()
        {
            return GetDatebase().GetCollection<UserEntity>("users");
        }
    }   
}
