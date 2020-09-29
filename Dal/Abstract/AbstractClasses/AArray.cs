using ConnectionRepository;
using Dal.Abstract.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Dal.Abstract.AbstractClasses
{
    public abstract class AArray<TEntity,TElem> : IArray<TEntity,TElem>
    {
        protected IMongoDatabase GetDatebase()
        {
            return new ClientRepository().GetClient().GetDatabase("SocialMedia");
        }
        protected abstract IMongoCollection<BsonDocument> GetCollection();
        protected abstract string GetArrayFieldName();


        public async Task<UpdateResult> PushAsync(TEntity Filetr, TElem Element)
        {
            return await GetCollection().UpdateOneAsync(Filetr.ToBsonDocument(), Builders<BsonDocument>.Update.Push(GetArrayFieldName(), Element));
        }
        public async Task<UpdateResult> PullAsync(TEntity Filetr, TElem Element)
        {
            return await GetCollection().UpdateOneAsync(Filetr.ToBsonDocument(), Builders<BsonDocument>.Update.Pull(GetArrayFieldName(), Element));
        }
        public async Task<int> ResetAsync(TEntity Filetr, TElem OldElement, TElem NewElement)
        {
            return await Task.Run(() =>
            {
                GetCollection().UpdateOne(Filetr.ToBsonDocument(), Builders<BsonDocument>.Update.Pull(GetArrayFieldName(), OldElement));
                GetCollection().UpdateOne(Filetr.ToBsonDocument(), Builders<BsonDocument>.Update.Push(GetArrayFieldName(), NewElement));
                return 1;
            });
        }
    }
}
