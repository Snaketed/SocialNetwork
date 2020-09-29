using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IFeels
    {
        Task<int> LikePostAsync(string postOwnerNickname, string feelerNickname, DateTime postDate);
        Task<int> DislikePostAsync(string postOwnerNickname, string feelerNickname, DateTime postDate);
        Task<UpdateResult> UndoFeelingAsync(string postOwnerNickname, string feelerNickname, DateTime postDate);
        Task<IList<FeelingModel>> GetPostLikersAsync(string postOwnerNickname, DateTime postDate);
        Task<IList<FeelingModel>> GetPostDislikersAsync(string postOwnerNickname, DateTime postDate);
    }
}
