using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IConverter
    {
        Task<ObjectId?> ToUserObjectIdOrNullAsync(string nickname);
        Task<string> ToNicknameOrNullAsync(ObjectId id);
        Task<UserModel> ToUserModelAsync(UserEntity Entity);
        Task<UserEntity> ToUserEntityAsync(UserModel Model);
        Task<PostModel> ToPostModelAsync(PostEntity Entity);
        Task<PostEntity> ToPostEntityAsync(PostModel Model);
        Task<ObjectId?> ToPostObjectIdOrNullAsync(string postOwnerNickname,DateTime postDate);
        Task<List<CommentModel>> ToPostCommentModelsListAsync(IList<CommentEntity> EntitiesList);
        Task<List<FeelingModel>> ToPostLiksModelsListAsync(IList<FeelingEntity> EntitiesList);
        Task<List<FeelingModel>> ToPostDislikesModelsListAsync(IList<FeelingEntity> EntitiesList);
    }
}
