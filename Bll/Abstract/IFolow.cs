using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IFolow
    {
        Task<int> FolowAsync(string nickname);
        Task<int> UnfolowAsync(string nickname);
    }
}
