using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IComment
    {
        Task<IList<CommentModel>> GetPostCommentAsync(string postOwnerNickname, DateTime postDate);
        Task<UpdateResult> CreateCommentAsync(string text, string postOwnerNickname, string commenterNickname, DateTime postDate);
        Task<UpdateResult> DeleteCommentAsync(string postOwnerNickname, string commenterNickname, DateTime postDate, DateTime commentDate);
        Task<int> EditCommentAsync(string postOwnerNickname, string commenterNickname, DateTime postDate, DateTime commentDate, string newText);
    }
}
