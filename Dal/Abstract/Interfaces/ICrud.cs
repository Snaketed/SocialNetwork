using Dal.Entity.Abstract;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Abstract.Interfaces
{
    public interface ICrud<TEntity> where TEntity : IEntity
    {
         Task InsertAsync(params TEntity[] Entities);
         Task<UpdateResult> UpdateAsync(TEntity Filter, TEntity Entity);
         Task<DeleteResult> DeleteAsync(TEntity Filter);
         IAsyncEnumerable<TEntity> SelectStreamAsync(TEntity Filter);
         Task<TEntity> SelectOneAsync(TEntity Filter);
    }
}
