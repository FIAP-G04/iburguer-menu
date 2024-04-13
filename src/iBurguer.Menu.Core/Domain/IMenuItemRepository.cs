namespace iBurguer.Menu.Core.Domain;

public interface IMenuRepository
{
    Task AddMenuItem(Item item);

    Task UpdateMenuItem(Item item);

    Task RemoveMenuItem(Item item);
    
    Task<Item?> GetMenuItemById(Id id, CancellationToken cancellationToken);

    Task<IEnumerable<Item>> GetMenuItemsByCategory(Category category,
        CancellationToken cancellationToken);
}