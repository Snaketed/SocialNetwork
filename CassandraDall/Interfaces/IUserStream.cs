using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using ModelsCassandra;

namespace CassandraDal.Interfaces
{
    public interface IUserStream
    {
       void SyncUserStream(ISession session, Guid postId, TimeUuid updatedAtPrev);
    }
}
