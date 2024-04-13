using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.MenuItemsByCategory;

public interface IGetCategorizedMenuItemsUseCase
{
    Task<IEnumerable<MenuItemResponse>> GetItemsByCategory(string categoryName,
        CancellationToken cancellation);
}

public class GetCategorizedMenuItemsUseCase : IGetCategorizedMenuItemsUseCase
{
    private readonly IMenuRepository _repository;

    public GetCategorizedMenuItemsUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<IEnumerable<MenuItemResponse>> GetItemsByCategory(string categoryName, CancellationToken cancellation)
    {
        var category = Category.FromName(categoryName);
        
        var items = await _repository.GetMenuItemsByCategory(category, cancellation);

        return items.Select(item => MenuItemResponse.Convert(item));
    }
}