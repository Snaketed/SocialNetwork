using Bll.Abstract;
using Bll.AbstractBll;
using Bll.HelperClasses;
using Dal.Abstract.Interfaces;
using Dal.Concrete;
using Dal.Concrete.PostArrays;
using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bll.Concrete
{
    public class PostBll : IPostBll
    {
        private readonly ICrud<PostEntity> PostCrud = new PostCrud();
        private readonly IArray<PostEntity, FeelingEntity> Feelings = new FeelingsArray();
        private readonly IArray<PostEntity, CommentEntity> Comments = new CommentsArray();
        private readonly IConverter Convert = new Converter();

        //Tested
        public async Task<UpdateResult> CreateCommentAsync(string text, string postOwnerNickname, string commenterNickname, DateTime postDate)
        {
            if(text !=null && postOwnerNickname!=null && commenterNickname!=null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? CommenterId = await Convert.ToUserObjectIdOrNullAsync(commenterNickname);
                        if (CommenterId != null)
                        {
                            return await Comments.PushAsync(Entity, new CommentEntity() { CommentOwnerId = CommenterId.Value, Date = DateTime.Now.ToString(), Text = text });
                        }
                    }
                }
            }
            return null;
        }
        //Tested
        public async Task<int> CtreatePostAsync(PostModel Post)
        {
            if (Post.PostOwnerNickname != null && Post.Text != null && Post.Text.Trim()!="")
            {
                ObjectId? id = await Convert.ToUserObjectIdOrNullAsync(Post.PostOwnerNickname);
                if (id != null)
                {
                    await PostCrud.InsertAsync(await Convert.ToPostEntityAsync(Post));
                    return 1;
                }
                return 0;
            }
            return 0;
        }
        //Tested
        public async Task<UpdateResult> DeleteCommentAsync(string postOwnerNickname, string commenterNickname, DateTime postDate, DateTime commentDate)
        {
            if (postOwnerNickname != null && commenterNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? CommenterId = await Convert.ToUserObjectIdOrNullAsync(commenterNickname);
                        if (CommenterId != null)
                        {
                            return await Comments.PullAsync(Entity, Entity.Comments.Where(entity => entity.Date == commentDate.ToString() && entity.CommentOwnerId == CommenterId).FirstOrDefault());
                        }
                    }
                }
            }
            return null;
        }
        //Tested
        public async Task<DeleteResult> DeletePostAsync(string postOwnerNickname, DateTime postDate)
        {
            if(postOwnerNickname!=null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    return await PostCrud.DeleteAsync(new PostEntity { Id = PostId.Value });
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        //Tested
        public async Task<int> DislikePostAsync(string postOwnerNickname, string feelerNickname, DateTime postDate)
        {
            if(postOwnerNickname!=null && feelerNickname!=null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? feelerId = await Convert.ToUserObjectIdOrNullAsync(feelerNickname);
                        if (feelerId != null)
                        {
                            FeelingEntity FeelingEntity = Entity?.Feelings.Where(f => f.FeelerId == feelerId).FirstOrDefault();
                            if (FeelingEntity != null)
                            {
                                if (FeelingEntity.Like != false)
                                {
                                    return await Feelings.ResetAsync(
                                        new PostEntity() { Id = Entity.Id },
                                        FeelingEntity,
                                        new FeelingEntity()
                                        {
                                            FeelerId = feelerId.Value,
                                            Like = false,
                                            Date = DateTime.Now.ToString()
                                        });
                                }
                            }
                            else
                            {
                                await Feelings.PushAsync(Entity,
                                new FeelingEntity()
                                {
                                    FeelerId = feelerId.Value,
                                    Like = false,
                                    Date = DateTime.Now.ToString()
                                });
                                return 1;
                            }
                        }
                    }
                }
            }       
            return 0;
        }
        //Tested
        public async Task<int> EditCommentAsync(string postOwnerNickname, string commenterNickname, DateTime postDate, DateTime commentDate, string newText)
        {
            if (newText!=null && postOwnerNickname != null && commenterNickname!=null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? CommenterId = await Convert.ToUserObjectIdOrNullAsync(commenterNickname);
                        if (CommenterId != null)
                        {
                            return await Comments.ResetAsync(
                                new PostEntity { Id = Entity.Id },
                                Entity.Comments.Where(entity => entity.Date == commentDate.ToString() && entity.CommentOwnerId == CommenterId).FirstOrDefault(),
                                new CommentEntity() { CommentOwnerId = CommenterId.Value, Date = commentDate.ToString(), Text = newText });
                        }
                    }
                }
            }
            return 0;
        }
        //Tested
        public async Task<UpdateResult> EditPostTextAsync(string postOwnerNickname, DateTime postDate, string newText)
        {
            if (postOwnerNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    return await PostCrud.UpdateAsync(new PostEntity { Id = PostId.Value }, new PostEntity { Text = newText });
                }
            }
            return null;            
        }
        //Tested
        public async Task<IList<CommentModel>> GetPostCommentAsync(string postOwnerNickname, DateTime postDate)
        {
            if (postOwnerNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        return await Convert.ToPostCommentModelsListAsync(Entity.Comments);
                    }
                }
            }
           return null;         
        }
        //Tested
        public async Task<IList<FeelingModel>> GetPostDislikersAsync(string postOwnerNickname, DateTime postDate)
        {
            if (postOwnerNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        return await Convert.ToPostDislikesModelsListAsync(Entity.Feelings);
                    }
                }
            }
            return null;
        }
        //Tested
        public async Task<IList<FeelingModel>> GetPostLikersAsync(string postOwnerNickname, DateTime postDate)
        {
            if (postOwnerNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        return await Convert.ToPostLiksModelsListAsync(Entity.Feelings);
                    }
                }
            }
            return null;
        }
        //Tested
        public async Task<List<PostModel>> GetPostsAsync(string postOwnerNickname)
        {
            List<PostModel> ModelsList = new List<PostModel>();
            if (postOwnerNickname != null)
            {
                ObjectId? PostOwnerId = await Convert.ToUserObjectIdOrNullAsync(postOwnerNickname);
                if (PostOwnerId != null)
                {
                    await foreach (PostEntity Entity in PostCrud.SelectStreamAsync(new PostEntity() { PostOwnerId = PostOwnerId.Value }))
                    {
                        ModelsList.Add(await Convert.ToPostModelAsync(Entity));
                    }
                }
            }
            return ModelsList.OrderByDescending(model=>model.Date).ToList();
        }
        //Tested
        public async Task<List<PostModel>> GetPostsAsync(IList<string> Nicknames)
        {
            List<PostModel> ModelsList = new List<PostModel>();
            foreach (string postOwnerNickname in Nicknames)
            {
                ObjectId? PostOwnerId = await Convert.ToUserObjectIdOrNullAsync(postOwnerNickname);
                if (PostOwnerId != null)
                {
                    await foreach (PostEntity Entity in PostCrud.SelectStreamAsync(new PostEntity() { PostOwnerId = PostOwnerId.Value }))
                    {
                        ModelsList.Add(await Convert.ToPostModelAsync(Entity));
                    }
                }
            }          
            return ModelsList.OrderByDescending(model => model.Date).ToList();
        }
        //Tested
        public async Task<List<PostModel>> GetPostsAsync()
        {
            List<PostModel> ModelsList = new List<PostModel>();
            await foreach (PostEntity Entity in PostCrud.SelectStreamAsync(new PostEntity() { }))
            {
                 ModelsList.Add(await Convert.ToPostModelAsync(Entity));
            }        
            return ModelsList.OrderByDescending(model => model.Date).ToList();
        }
        //Tested
        public async Task<int> LikePostAsync(string postOwnerNickname, string feelerNickname, DateTime postDate)
        {
            if (postOwnerNickname != null && feelerNickname != null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? feelerId = await Convert.ToUserObjectIdOrNullAsync(feelerNickname);                  
                        if (feelerId != null)
                        {
                            FeelingEntity FeelingEntity = Entity?.Feelings.Where(f => f.FeelerId == feelerId).FirstOrDefault();
                            if (FeelingEntity != null)
                            {
                                if (FeelingEntity.Like != true)
                                {
                                    return await Feelings.ResetAsync(
                                             new PostEntity() { Id = Entity.Id },
                                             FeelingEntity,
                                             new FeelingEntity()
                                             {
                                                 FeelerId = feelerId.Value,
                                                 Like = true,
                                                 Date = DateTime.Now.ToString()
                                             });
                                }
                            }
                            else
                            {
                                await Feelings.PushAsync(
                                    Entity,
                                    new FeelingEntity()
                                    {
                                        FeelerId = feelerId.Value,
                                        Like = true,
                                        Date = DateTime.Now.ToString()
                                    });
                                return 1;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        //Tested
        public async Task<UpdateResult> UndoFeelingAsync(string postOwnerNickname, string feelerNickname, DateTime postDate)
        {
            if (postOwnerNickname != null && feelerNickname!=null)
            {
                ObjectId? PostId = await Convert.ToPostObjectIdOrNullAsync(postOwnerNickname, postDate);
                if (PostId != null)
                {
                    PostEntity Entity = await PostCrud.SelectOneAsync(new PostEntity { Id = PostId.Value });
                    if (Entity != null)
                    {
                        ObjectId? feelerId = await Convert.ToUserObjectIdOrNullAsync(feelerNickname);
                        if (feelerId != null)
                        {
                            return await Feelings.PullAsync(Entity, Entity.Feelings.Where(entity => entity.FeelerId == feelerId).FirstOrDefault());
                        }
                    }
                }
            }
            return null;
        }
    }
}
