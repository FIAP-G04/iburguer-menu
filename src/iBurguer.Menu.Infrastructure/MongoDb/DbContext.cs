using iBurguer.Menu.Infrastructure.MongoDb.Configurations;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace iBurguer.Menu.Infrastructure.MongoDB;

public class DbContext : IDbContext
{
    public IMongoClient Client { get; }
    public IMongoDatabase Database { get; }
    
    public DbContext(MongoDbConfiguration configuration)
    {
        var url = new MongoUrl(configuration.ConnectionString);
        var settings = MongoClientSettings.FromUrl(url);
        var options = new InstrumentationOptions { CaptureCommandText = true };

        settings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber(options));

        Client = new MongoClient(settings);
        Database = Client.GetDatabase(configuration.Database);
    }
}