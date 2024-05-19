using AutoFixture;
using FluentAssertions;
using iBurguer.Menu.Infrastructure.MongoDb.Configurations;
using iBurguer.Menu.UnitTests.Util;

namespace iBurguer.Menu.UnitTests.Infrastructure.MongoDb;

public class MongoDbConfigurationTests : BaseTests
{
    [Fact]
    public void ShouldThrowExceptionWhenConnectionStringIsNotProvided()
    {
        // Arrange
        var configuration = new MongoDbConfiguration
        {
            ConnectionString = string.Empty,
            Database = Fake.Create<string>()
        };

        // Act
        Action act = () => configuration.ThrowIfInvalid();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenDatabaseIsNotProvided()
    {
        // Arrange
        var configuration = new MongoDbConfiguration
        {
            ConnectionString = Fake.Create<string>(),
            Database = string.Empty
        };

        // Act
        Action act = () => configuration.ThrowIfInvalid();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldCreateAnValidInstanceOfMongoDbConfiguration()
    {
        // Arrange
        var configuration = new MongoDbConfiguration
        {
            ConnectionString = Fake.Create<string>(),
            Database = Fake.Create<string>()
        };

        // Act & Assert
        configuration.Invoking(c => c.ThrowIfInvalid()).Should().NotThrow();
        configuration.ConnectionString.Should().NotBeEmpty();
        configuration.Database.Should().NotBeEmpty();
    }
}