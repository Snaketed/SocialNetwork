using Bll.Abstract;
using Models;

namespace Bll.AbstractBll
{
    public interface IUserBll : ILogIn, ISignUp, IFolow, IProfile, IRefresh
    {
        bool UserLogged { get; }
        UserModel User { get; }
    }
}
