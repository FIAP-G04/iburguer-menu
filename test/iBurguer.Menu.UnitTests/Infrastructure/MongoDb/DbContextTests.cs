using FluentAssertions;
using iBurguer.Menu.Infrastructure.MongoDb.Configurations;
using iBurguer.Menu.Infrastructure.MongoDB;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace iBurguer.Menu.UnitTests.Infrastructure.MongoDb;

public class DbContextTests
{
    [Fact]
    public void DbContext_ShouldInitializeClientAndDatabase()
    {
        // Arrange
        var configuration = new MongoDbConfiguration
        {
            ConnectionString = "mongodb://localhost:27017",
            Database = "testDb"
        };

        // Act
        var context = new DbContext(configuration);

        // Assert
        context.Client.Should().NotBeNull();
        context.Database.Should().NotBeNull();
        context.Database.DatabaseNamespace.DatabaseName.Should().Be(configuration.Database);
    }

    //[Fact]
    //public void DbContext_ShouldConfigureMongoClientSettings()
    //{
    //    // Arrange
    //    var configuration = new MongoDbConfiguration
    //    {
    //        ConnectionString = "mongodb://localhost:27017",
    //        Database = "testDb"
    //    };

    //    // Act
    //    var context = new DbContext(configuration);
    //    var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(configuration.ConnectionString));

    //    // Assert
    //    context.Client.Settings.Should().BeEquivalentTo(clientSettings, options =>
    //        options.Excluding(s => s.ClusterConfigurator));
    //}

    [Fact]
    public void DbContext_ShouldSetInstrumentationOptions()
    {
        // Arrange
        var configuration = new MongoDbConfiguration
        {
            ConnectionString = "mongodb://localhost:27017",
            Database = "testDb"
        };

        // Act
        var context = new DbContext(configuration);
        var options = new InstrumentationOptions { CaptureCommandText = true };

        // Assert
        context.Client.Settings.ClusterConfigurator.Should().NotBeNull();
    }
}