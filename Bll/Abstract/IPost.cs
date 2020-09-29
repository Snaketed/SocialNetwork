using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IPost
    {
        Task<List<PostModel>> GetPostsAsync(string postOwnerNickname);
        Task<List<PostModel>> GetPostsAsync(IList<string> PostOwnerNicknames);
        Task<List<PostModel>> GetPostsAsync();
        Task<int> CtreatePostAsync(PostModel Post);
        Task<DeleteResult> DeletePostAsync(string postOwnerNickname, DateTime postDate);
        Task<UpdateResult> EditPostTextAsync(string postOwnerNickname, DateTime postDate, string newText);
    }
}
