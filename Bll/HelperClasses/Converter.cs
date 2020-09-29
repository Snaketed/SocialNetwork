using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bll.Abstract;
using Dal.Abstract.Interfaces;
using Dal.Concrete;
using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using Models;
using MongoDB.Bson;

namespace Bll.HelperClasses
{
    public class Converter : IConverter
    {
        readonly ICrud<UserEntity> UserCrud = new UserCrud();
        readonly ICrud<PostEntity> PostCrud = new PostCrud();
        public async Task<string> ToNicknameOrNullAsync(ObjectId id)
        {
            UserEntity Entity = new UserEntity();
            if(id != ObjectId.Empty)
            {
                Entity = await UserCrud.SelectOneAsync(new UserEntity() { Id = id });
            }
            return Entity?.Nickname;
        }

        public async Task<ObjectId?> ToUserObjectIdOrNullAsync(string nickname)
        {
            UserEntity Entity=new UserEntity();
            if (nickname!=null)
            {
                Entity = await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname });
            }
            return Entity?.Id;
        }

        public async Task<PostEntity> ToPostEntityAsync(PostModel Model)
        {
            if (Model.Comments == null)
            {
                Model.Comments = new List<CommentModel>();
            }
            if (Model.Likes == null)
            {
                Model.Likes = new List<FeelingModel>();
            }
            if (Model.Dislikes == null)
            {
                Model.Dislikes = new List<FeelingModel>();
            }
            ObjectId? id = await ToUserObjectIdOrNullAsync(Model.PostOwnerNickname);
            if (Model?.PicturePath != null && File.Exists(Model.PicturePath))
            {
                File.Copy(Model.PicturePath, @"Source\"+ Model.PicturePath.Split('\\').Last());
                Model.PicturePath = @"Source\" + Model.PicturePath.Split('\\').Last();
            }
            else
            {
                Model.PicturePath = "No picture pass info";
            }
            PostEntity Entity = new PostEntity()
            {
                Text=Model.Text,
                PicturePath = Model.PicturePath,
                Date=Model.Date.ToString(),
                PostOwnerId = id.Value,
                Feelings=new List<FeelingEntity>(),
                Comments=new List<CommentEntity>()
            };
            foreach (var Feeling in Model.Dislikes)
            {
                id= await ToUserObjectIdOrNullAsync(Feeling.FeelingOwnerNickname);
                Entity.Feelings.Add(new FeelingEntity
                {
                    Like = false,
                    Date = Feeling.Date.ToString(),
                    FeelerId = id.Value
                });
            }
            foreach (var Feeling in Model.Likes)
            {
                id = await ToUserObjectIdOrNullAsync(Feeling.FeelingOwnerNickname);
                Entity.Feelings.Add(new FeelingEntity
                {
                    Like = true,
                    Date = Feeling.Date.ToString(),
                    FeelerId = id.Value
                });
            }
            foreach (var Comment in Model.Comments)
            {
                id = await ToUserObjectIdOrNullAsync(Comment.CommentOwnerNickname);
                Entity.Comments.Add(new CommentEntity
                {
                    Text=Comment.Text,
                    Date = Comment.Date.ToString(),
                    CommentOwnerId=id.Value
                });
            }
            return Entity;
        }

        public async Task<PostModel> ToPostModelAsync(PostEntity Entity)
        {
            string nickname = await ToNicknameOrNullAsync(Entity.PostOwnerId);
            PostModel Model = new PostModel()
            {
                Text = Entity.Text,
                PicturePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)+"\\" + Entity.PicturePath,
                Date = Convert.ToDateTime(Entity.Date),
                PostOwnerNickname = nickname,
                Comments = await ToPostCommentModelsListAsync(Entity.Comments),
                Likes = await ToPostLiksModelsListAsync(Entity.Feelings),
                Dislikes = await ToPostDislikesModelsListAsync(Entity.Feelings)
            };
            return Model;
        }

        public async Task<UserEntity> ToUserEntityAsync(UserModel Model)
        {
            if(Model.SubscribersNicknames==null)
            {
                Model.SubscribersNicknames = new List<string>();
            }
            if(Model.FavoriteUsersNicknames == null)
            {
                Model.FavoriteUsersNicknames = new List<string>();
            }
            UserEntity Entity = new UserEntity()
            {
                Nickname = Model.Nickname,
                Password = Model.Password,
                PhoneNumber = Model.PersonInfo?.PhoneNumber ?? "No phone number info",
                MailAddress = Model.PersonInfo?.MailAddress ?? "No mail address info",
                FirstName = Model.PersonInfo.FirstName,
                LastName = Model.PersonInfo.LastName,
                BornDate = Model.PersonInfo.BornDate.ToString(),
                Subscribers = new List<ObjectId>(),
                FavoriteUsers = new List<ObjectId>()
            };
            ObjectId? id;
            foreach(var nickname in Model.SubscribersNicknames)
            {
                id = await ToUserObjectIdOrNullAsync(nickname);
                Entity.Subscribers.Add(id.Value);
            }
            foreach (var nickname in Model.FavoriteUsersNicknames)
            {
                id = await ToUserObjectIdOrNullAsync(nickname);
                Entity.FavoriteUsers.Add(id.Value);
            }
            return Entity;
        }

        public async Task<UserModel> ToUserModelAsync(UserEntity Entity)
        {
            UserModel Model = new UserModel()
            {
                Nickname = Entity.Nickname,
                Password = Entity.Password,
                PersonInfo=new PersonInfoModel()
                {
                    PhoneNumber = Entity.PhoneNumber,
                    MailAddress = Entity.MailAddress,
                    FirstName = Entity.FirstName,
                    LastName = Entity.LastName,
                    BornDate = Convert.ToDateTime(Entity.BornDate),
                },
                SubscribersNicknames = new List<string>(),
                FavoriteUsersNicknames = new List<string>()
            };
            string nickname;
            foreach (var id in Entity.Subscribers)
            {
                nickname = await ToNicknameOrNullAsync(id);
                Model.SubscribersNicknames.Add(nickname);
            }
            foreach (var id in Entity.FavoriteUsers)
            {
                nickname = await ToNicknameOrNullAsync(id);
                Model.FavoriteUsersNicknames.Add(nickname);
            }
            return Model;
        }

        public async Task<ObjectId?> ToPostObjectIdOrNullAsync(string postOwnerNickname, DateTime postDate)
        {
            PostEntity Entity = new PostEntity();
            if (postOwnerNickname!=null)
            {
                ObjectId? id = await ToUserObjectIdOrNullAsync(postOwnerNickname);
                Entity = await PostCrud.SelectOneAsync(new PostEntity() { PostOwnerId = id.Value, Date = postDate.ToString() });
            }
            return Entity?.Id;
        }

        public async Task<List<CommentModel>> ToPostCommentModelsListAsync(IList<CommentEntity> EntitiesList)
        {
            List<CommentModel> ModelsList = new List<CommentModel>();
            string nickname;
            foreach (var Comment in EntitiesList)
            {
                nickname = await ToNicknameOrNullAsync(Comment.CommentOwnerId);
                ModelsList.Add(new CommentModel()
                {
                    Text = Comment.Text,
                    Date = Convert.ToDateTime(Comment.Date),
                    CommentOwnerNickname = nickname
                });
            }
            return ModelsList;
        }

        public async Task<List<FeelingModel>> ToPostLiksModelsListAsync(IList<FeelingEntity> EntitiesList)
        {
            List<FeelingModel> ModelsList = new List<FeelingModel>();
            string nickname;
            foreach (var Feeling in EntitiesList.Where(entity=>entity.Like == true).OrderBy(entity=>entity.Date))
            {
                nickname = await ToNicknameOrNullAsync(Feeling.FeelerId);
                ModelsList.Add(new FeelingModel()
                {
                    Date = Convert.ToDateTime(Feeling.Date),
                    FeelingOwnerNickname = nickname
                });
            }
            return ModelsList;
        }

        public async Task<List<FeelingModel>> ToPostDislikesModelsListAsync(IList<FeelingEntity> EntitiesList)
        {
            List<FeelingModel> ModelsList = new List<FeelingModel>();
            string nickname;
            foreach (var Feeling in EntitiesList.Where(entity => entity.Like == false).OrderBy(entity => entity.Date))
            {
                nickname = await ToNicknameOrNullAsync(Feeling.FeelerId);
                ModelsList.Add(new FeelingModel()
                {
                    Date = Convert.ToDateTime(Feeling.Date),
                    FeelingOwnerNickname = nickname
                });
            }
            return ModelsList;
        }
    }
}
