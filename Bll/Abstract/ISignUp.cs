using Models;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface ISignUp
    {
        Task<int> SignUpAsync(UserModel Model);
    }
}
