using MongoDB.Driver;

namespace iBurguer.Menu.Infrastructure.MongoDB;

public interface IDbContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
}