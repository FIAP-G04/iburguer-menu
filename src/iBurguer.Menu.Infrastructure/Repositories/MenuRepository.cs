using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace iBurguer.Menu.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IMongoCollection<Item> _collection;
    
    public MenuRepository(IDbContext mongoDbContext)
    {
        _collection = mongoDbContext.Database.GetCollection<Item>("items");
    }
    
    public async Task AddMenuItem(Item item)
    {
        await _collection.InsertOneAsync(item, null);
    }

    public async Task UpdateMenuItem(Item item)
    {
        var update = Builders<Item>.Update
            .Set(p => p.Name, item.Name)
            .Set(p => p.Description, item.Description)
            .Set(p => p.Category, item.Category)
            .Set(p => p.Price, item.Price)
            .Set(p => p.PreparationTime, item.PreparationTime)
            .Set(p => p.Enabled, item.Enabled)
            .Set(p => p.Images, item.Images)
            .Set(p => p.UpdatedAt, item.UpdatedAt);

        await _collection.UpdateOneAsync(i => i.Id == item.Id, update, null);
    }

    public async Task RemoveMenuItem(Item item)
    {
        await _collection.DeleteOneAsync(i => i.Id == item.Id);
    }

    public async Task<Item?> GetMenuItemById(Id id, CancellationToken cancellationToken)
    {
        return await _collection.Find(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetMenuItemsByCategory(Category category, CancellationToken cancellationToken)
    {
        return await _collection.Find(i => i.Enabled && i.Category == category).ToListAsync(cancellationToken);
    }
}