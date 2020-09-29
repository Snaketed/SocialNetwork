using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Abstract;
using Bll.AbstractBll;
using Bll.HelperClasses;
using Dal.Abstract.Interfaces;
using Dal.Concrete;
using Dal.Concrete.PostArrays;
using Dal.Concrete.UserArrays;
using Dal.Entity.Concrete;
using Dal.Entity.Concrete.PostArrayEntities;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bll.Concrete
{
    public class UserBll : IUserBll
    {
        private readonly ICrud<UserEntity> UserCrud = new UserCrud();
        private readonly IArray<UserEntity, ObjectId> Subscribers = new SubscribersArray();
        private readonly IArray<UserEntity, ObjectId> FavoriteUsers = new FavoriteUsersArray();

        private readonly ICrud<PostEntity> PostCrud = new PostCrud();
        private readonly IArray<PostEntity, FeelingEntity> Feelings = new FeelingsArray();
        private readonly IArray<PostEntity, CommentEntity> Comments = new CommentsArray();

        private readonly IConverter Convert = new Converter();
        private readonly IHashing Md5 = new HashMd5();

        public bool UserLogged { get; private set; } = false;
        public UserModel User { get; private set; }

        public UserBll()
        {
            User = new UserModel()
            {
                SubscribersNicknames = new List<string>(),
                FavoriteUsersNicknames = new List<string>()
            };
        }
        //Tested
        public async Task<int> FolowAsync(string nickname)
        {
            if (UserLogged && nickname != null && nickname != User.Nickname && !User.FavoriteUsersNicknames.Contains(nickname))
            {
                ObjectId? Id = await Convert.ToUserObjectIdOrNullAsync(nickname);
                if (Id != null)
                {
                    await FavoriteUsers.PushAsync(new UserEntity { Nickname = User.Nickname }, Id.Value);
                    Id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                    await Subscribers.PushAsync(new UserEntity { Nickname = nickname }, Id.Value);
                    User.FavoriteUsersNicknames.Add(nickname);
                    return 1;
                }
            }
            return 0;
        }
        //Tested
        public async Task<List<string>> GetFavoriteUsersNicknamesAsync(string nickname)
        {
            if (nickname != null)
            {
                UserModel Model = await Convert.ToUserModelAsync(await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname }));
                return Model.FavoriteUsersNicknames as List<string>;
            }
            return null;
        }
        //Tested
        public async Task<List<string>> GetSubscribersNicknamesAsync(string nickname)
        {
            if (nickname != null)
            {
                UserModel Model = await Convert.ToUserModelAsync(await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname }));
                return Model.SubscribersNicknames as List<string>;
            }
            return null;
        }
        //Tested
        public async Task<PersonInfoModel> GetProfileInfoAsync(string nickname)
        {
            if(nickname != null)
            {
                UserModel Model =await Convert.ToUserModelAsync( await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname }));
                return Model.PersonInfo;
            }
            return null;
        }
        //Tested
        public async Task<int> LogInAsync(string nickname, string password)
        {
            UserEntity Entity = await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname, Password = Md5.GetHashString(password) })
                ?? await UserCrud.SelectOneAsync(new UserEntity() { Nickname = nickname, Password = password });
            if (Entity != null)
            {
                User = await Convert.ToUserModelAsync(Entity);
                UserLogged = true;
                return 1;
            }
            else
            {
                User = new UserModel()
                {
                    SubscribersNicknames = new List<string>(),
                    FavoriteUsersNicknames = new List<string>()
                };
                UserLogged = false;
                return 0;
            }
        }
        //Tested
        public async Task LogOutAsync()
        {
            await Task.Run(() =>
            {
                User = new UserModel()
                {
                    SubscribersNicknames = new List<string>(),
                    FavoriteUsersNicknames = new List<string>()
                };
                UserLogged = false;
            });
        }
        //Tested
        public async Task RefreshAsync()
        {
            if (User.Nickname != null && User.Password != null)
            {
                await LogInAsync(User.Nickname, User.Password);
            }
        }
        //Tested
        public async Task<int> SignUpAsync(UserModel Model)
        {
            if (await UserCrud.SelectOneAsync(new UserEntity { Nickname = Model.Nickname }) == null)
            {
                Model.Password = Md5.GetHashString(Model.Password);
                await UserCrud.InsertAsync(await Convert.ToUserEntityAsync(Model));
                return 1;
            }
            return 0;
        }
        //Tested
        public async Task<int> UnfolowAsync(string nickname)
        {
            ObjectId? id = await Convert.ToUserObjectIdOrNullAsync(nickname);
            if (UserLogged && id != null && nickname != User.Nickname && User.FavoriteUsersNicknames.Contains(nickname))
            {
                await FavoriteUsers.PullAsync(new UserEntity { Nickname = User.Nickname }, id.Value);
                id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                await Subscribers.PullAsync(new UserEntity { Nickname = nickname }, id.Value);
                User.FavoriteUsersNicknames.Remove(nickname);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //Tested
        public async Task<UpdateResult> UpdetePersonInfoAsync(PersonInfoModel Model)
        {
            if (UserLogged && Model.FirstName!=null && Model.LastName!=null && Model.BornDate != new DateTime() && Model.FirstName.Trim() != "" && Model.LastName.Trim() != "")
            {
                ObjectId? Id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                if (Id != null)
                {
                    User.PersonInfo = new PersonInfoModel()
                    {
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        PhoneNumber = Model?.PhoneNumber,
                        MailAddress = Model?.MailAddress,
                        BornDate = Model.BornDate
                    };
                    return await UserCrud.UpdateAsync(
                        new UserEntity { Id = Id.Value },
                        new UserEntity
                        {
                            FirstName = User.PersonInfo.FirstName,
                            LastName = User.PersonInfo.LastName,
                            PhoneNumber = User.PersonInfo?.PhoneNumber,
                            MailAddress = User.PersonInfo?.MailAddress,
                            BornDate = User.PersonInfo.BornDate.ToString()
                        });
                }
            }
            return null;
        }
        //Tested
        public async Task<UpdateResult> EditNickameAsync(string newNickname)
        {
            if (UserLogged && newNickname!=null && newNickname.Trim()!="")
            {
                ObjectId? Id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                if (Id != null)
                {
                    User.Nickname = newNickname;
                    return await UserCrud.UpdateAsync(new UserEntity { Id = Id.Value }, new UserEntity { Nickname = newNickname });
                }
            }
            return null;
        }
        //Tested
        public async Task<UpdateResult> EditPasswordAsync(string oldPassword, string newPassword)
        {
            if (UserLogged && newPassword!=null && newPassword.Trim() != "")
            {
                if (Md5.GetHashString(oldPassword) == User.Password)
                {
                    ObjectId? Id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                    if (Id != null)
                    {
                        User.Password = Md5.GetHashString(newPassword);
                        return await UserCrud.UpdateAsync(new UserEntity { Id = Id.Value }, new UserEntity { Password = User.Password });
                    }
                }
            }
            return null;
        }
        //Tested
        public async Task<UpdateResult> EditPfofileAsync(PersonInfoModel PersonInfo)
        {
            if (UserLogged && PersonInfo != null)
            {
                ObjectId? Id = await Convert.ToUserObjectIdOrNullAsync(User.Nickname);
                if (Id != null)
                {
                    User.PersonInfo = PersonInfo;
                    return await UserCrud.UpdateAsync(new UserEntity { Id = Id.Value }, new UserEntity { LastName = PersonInfo.LastName,FirstName= PersonInfo.FirstName,MailAddress= PersonInfo.MailAddress,PhoneNumber= PersonInfo.PhoneNumber,BornDate= PersonInfo.BornDate.ToString() });
                }
            }
            return null;
        }
        //Tested
        public async Task<DeleteResult> DeleteProfileAsync(string nickname)
        {
            if (UserLogged)
            {
                if (nickname==User.Nickname)
                {
                    ObjectId? id = await Convert.ToUserObjectIdOrNullAsync(nickname);
                    if (id != null)
                    {
                        await foreach(PostEntity Entity in PostCrud.SelectStreamAsync(new PostEntity() { }))
                        {
                            foreach(FeelingEntity feeling in Entity.Feelings.Where(f=>f.FeelerId==id))
                            {
                                await Feelings.PushAsync(Entity, feeling);
                            }
                            foreach (CommentEntity comment in Entity.Comments.Where(f => f.CommentOwnerId == id))
                            {
                                await Comments.PullAsync(Entity, comment);
                            }
                        }
                        await foreach (UserEntity Entity in UserCrud.SelectStreamAsync(new UserEntity() { }))
                        {
                            await Subscribers.PullAsync(Entity, id.Value);
                        }
                        await foreach (PostEntity Entity in PostCrud.SelectStreamAsync(new PostEntity() {PostOwnerId=id.Value }))
                        {
                            await PostCrud.DeleteAsync(Entity);
                        }
                        await LogOutAsync();
                        return await UserCrud.DeleteAsync(new UserEntity() { Id = id.Value });
                    }
                }
            }
            return null;
        }
    }
}
