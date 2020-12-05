using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using CassandraDal.Interfaces;

namespace CassandraDal.Concrete
{
    public class Posts : IPosts
    {
        private readonly IUserStream _userStream;
        public void CreatePost(ISession session)
        {

            Guid postId = Guid.NewGuid();
            Guid authorId = Guid.NewGuid();
            TimeUuid updatedAt = TimeUuid.NewId(DateTimeOffset.Now);

            Console.Write("Enter post : ");
            string content = Console.ReadLine();


            var insertPost = session.Prepare(
                "INSERT INTO posts (post_id, author_id,content, updated_at) VALUES(?,?,?, ?)");
            session.Execute(insertPost.Bind(postId, authorId, content, updatedAt));

            var insertPostFollowers = session.Prepare(
                "INSERT INTO post_followers (post_id, followers) VALUES(?,?)");

            _userStream.SyncUserStream(session, postId, updatedAt);


        }
        public  void UpdatePost(ISession session)
        {


            TimeUuid updatedAt = TimeUuid.NewId(DateTimeOffset.Now);


            Console.Write("Enter post_id : ");
            string strPostId = Console.ReadLine();
            Guid postId = new Guid(strPostId);


            Console.Write("Edit post : ");
            string content = Console.ReadLine();

            TimeUuid updatedAtPrev = new TimeUuid();
            var getLastUpdate = session.Prepare("Select updated_at from posts where post_id = ? ");
            var ex = session.Execute(getLastUpdate.Bind(postId));
            foreach (var lastUpdateAt in ex)
            {
                updatedAtPrev = lastUpdateAt.GetValue<TimeUuid>("updated_at");
            }


            var updatedPost = session.Prepare(
                "Update posts set content = ?, updated_at=?  where post_id = ?");
            session.Execute(updatedPost.Bind(content, updatedAt, postId));


            _userStream.SyncUserStream(session, postId, updatedAtPrev);

        }

    }
}
