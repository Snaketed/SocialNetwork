using MongoDB.Driver;

namespace ConnectionRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly string connectionString= "mongodb://localhost:27017";
        private static object locker;

        public IMongoClient GetClient()
        {
            return new MongoClient(connectionString);
        }
    }
}
