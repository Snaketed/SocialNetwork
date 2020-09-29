using System.Collections.Generic;

namespace Models
{
    public class UserModel
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public PersonInfoModel PersonInfo { get; set; }
        public IList<string> SubscribersNicknames { get; set; }
        public IList<string> FavoriteUsersNicknames { get; set; }
        public UserModel()
        {
            PersonInfo = new PersonInfoModel();
        }
    }
}
