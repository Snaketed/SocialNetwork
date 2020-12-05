using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace CassandraDal.Interfaces
{
    public interface IComment
    {
        public  void AddComment(ISession session);
    }
}
