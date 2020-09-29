using Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IProfile
    {
        Task<PersonInfoModel> GetProfileInfoAsync(string nickname);
        Task<UpdateResult> UpdetePersonInfoAsync(PersonInfoModel Model);
        Task<UpdateResult> EditNickameAsync(string newNickname);
        Task<UpdateResult> EditPasswordAsync(string oldPassword, string newPassword);
        Task<UpdateResult> EditPfofileAsync(PersonInfoModel PersonInfo);
        Task<List<string>> GetSubscribersNicknamesAsync(string nickname);
        Task<List<string>> GetFavoriteUsersNicknamesAsync(string nickname);
        Task<DeleteResult> DeleteProfileAsync(string nickname);
    } 
}
