using MongoDB.Driver;

namespace ConnectionRepository
{
    public interface IClientRepository
    {
        public IMongoClient GetClient();
    }
}
