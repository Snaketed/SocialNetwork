using Models;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface ILogIn
    {
        Task<int> LogInAsync(string nickname, string password);
        Task LogOutAsync();
    }
}
