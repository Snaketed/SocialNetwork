using MongoDB.Driver;
using System.Threading.Tasks;

namespace Dal.Abstract.Interfaces
{
    public interface IArray<TEntity,TElem>
    {
        Task<UpdateResult> PushAsync(TEntity Filter, TElem Element);
        Task<UpdateResult> PullAsync(TEntity Filter, TElem Element);
        Task<int> ResetAsync(TEntity Filter, TElem OldElement, TElem NewElement);
    }
}
