using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using CassandraDal.Interfaces;

namespace CassandraDal.Concrete
{
    public class Comment
    {
        private readonly IUserStream _userStream;
        public  void AddComment(ISession session)
        {
            TimeUuid comment_id = TimeUuid.NewId(DateTimeOffset.Now);
            Guid userId = Guid.NewGuid();

            Console.Write("Enter post_id : ");
            string strPostId = Console.ReadLine();
            Guid postId = new Guid(strPostId);

            Console.Write("Enter post : ");
            string comment = Console.ReadLine();

            var addComment = session.Prepare(
                "INSERT INTO comments (post_id, comment_id , comment, user_id) VALUES(?,?,?, ?)");
            session.Execute(addComment.Bind(postId, comment_id, comment, userId));

            var updatedPost = session.Prepare(
                "Update posts set updated_at=?  where post_id = ?");
            session.Execute(updatedPost.Bind(comment_id, postId));

            TimeUuid updatedAtPrev = new TimeUuid();
            var getLastUpdate = session.Prepare("Select updated_at from posts where post_id = ? ");
            var ex = session.Execute(getLastUpdate.Bind(postId));
            foreach (var lastUpdateAt in ex)
            {
                updatedAtPrev = lastUpdateAt.GetValue<TimeUuid>("updated_at");
            }

            _userStream.SyncUserStream(session, postId, updatedAtPrev);


        }
    }
}
