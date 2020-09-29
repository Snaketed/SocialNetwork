using ConnectionRepository;
using Dal.Abstract.Interfaces;
using Dal.Entity.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Abstract.AbstractClasses
{
    public abstract class ACrud<TEntity> : ICrud<TEntity> where TEntity : class, IEntity, new()
    {
        protected IMongoDatabase GetDatebase()
        {
            return new ClientRepository().GetClient().GetDatabase("SocialMedia");
        }
        protected abstract IMongoCollection<TEntity> GetCollection();

        public async Task InsertAsync(params TEntity[] Entities)
        {
             await GetCollection().InsertManyAsync(Entities);
        }

        public async Task<UpdateResult> UpdateAsync(TEntity Filter, TEntity Entity)
        {
            return await GetCollection().UpdateManyAsync(Filter.ToJson(), new BsonDocument("$set", Entity.ToBsonDocument()));
        }

        public async Task<DeleteResult> DeleteAsync(TEntity Filter)
        {
            return await GetCollection().DeleteManyAsync(Filter.ToJson());
        }

        public async IAsyncEnumerable<TEntity> SelectStreamAsync(TEntity Filter)
        {
            var cursor = await GetCollection().FindAsync(Filter.ToJson());
            while(await cursor.MoveNextAsync())
            {
                var Entities = cursor.Current;
                foreach (TEntity Entity in Entities)
                {
                    yield return Entity;
                }
            }
        }

        public async Task<TEntity> SelectOneAsync(TEntity Filter)
        {
            try
            {
                return await GetCollection().Find(Filter.ToJson()).SingleAsync();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
