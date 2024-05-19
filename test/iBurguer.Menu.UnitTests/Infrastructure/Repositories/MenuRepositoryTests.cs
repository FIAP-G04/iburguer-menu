using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Infrastructure.MongoDB;
using iBurguer.Menu.Infrastructure.Repositories;
using iBurguer.Menu.UnitTests.Util;
using MongoDB.Driver;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.Infrastructure.Repositories;

public class MenuRepositoryTests : BaseTests
{
    private readonly MenuRepository _sut;
    private readonly IMongoCollection<Item> _collection;
    private readonly IDbContext _dbContext;

    public MenuRepositoryTests()
    {
        _dbContext = Substitute.For<IDbContext>();
        _collection = Substitute.For<IMongoCollection<Item>>();
        _dbContext.Database.GetCollection<Item>("items").Returns(_collection);
        _sut = new MenuRepository(_dbContext);
    }

    [Fact]
    public async Task AddMenuItem_ShouldCallInsertOneAsync()
    {
        // Arrange
        var item = new Item("Test Item", "Description", new Price(10), new Category(1, "Test Category"), 15, new List<Url>());

        // Act
        await _sut.AddMenuItem(item);

        // Assert
        await _collection.Received(1).InsertOneAsync(item, null);
    }

    [Fact]
    public async Task UpdateMenuItem_ShouldCallUpdateOneAsync()
    {
        // Arrange
        var item = new Item("Test Item", "Description", new Price(10), new Category(1, "Test Category"), 15, new List<Url>());
        var updateDefinition = Builders<Item>.Update
            .Set(p => p.Name, item.Name)
            .Set(p => p.Description, item.Description)
            .Set(p => p.Category, item.Category)
            .Set(p => p.Price, item.Price)
            .Set(p => p.PreparationTime, item.PreparationTime)
            .Set(p => p.Enabled, item.Enabled)
            .Set(p => p.Images, item.Images)
            .Set(p => p.UpdatedAt, item.UpdatedAt);

        // Act
        await _sut.UpdateMenuItem(item);

        // Assert
        await _collection.Received(1).UpdateOneAsync(Arg.Is<FilterDefinition<Item>>(filter => true), Arg.Is<UpdateDefinition<Item>>(update => true), null);
    }

    [Fact]
    public async Task RemoveMenuItem_ShouldCallDeleteOneAsync()
    {
        // Arrange
        var item = new Item("Test Item", "Description", new Price(10), new Category(1, "Test Category"), 15, new List<Url>());

        // Act
        await _sut.RemoveMenuItem(item);

        // Assert
        await _collection.Received(1).DeleteOneAsync(Arg.Is<FilterDefinition<Item>>(filter => true));
    }

}