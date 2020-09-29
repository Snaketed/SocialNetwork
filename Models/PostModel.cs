using System;
using System.Collections.Generic;

namespace Models
{
    public class PostModel
    {
        public string Text { get; set; }
        public string PicturePath { get; set; }
        public DateTime Date { get; set; }
        public string PostOwnerNickname { get; set; }
        public IList<FeelingModel> Likes { get; set; }
        public IList<FeelingModel> Dislikes { get; set; }
        public IList<CommentModel> Comments { get; set; }
    }
}
