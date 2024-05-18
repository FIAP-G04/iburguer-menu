using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.MenuItemsByCategory;

namespace iBurguer.Menu.Core.UseCases.GetMenuItem;

public interface IGetMenuItemUseCase
{
    Task<MenuItemResponse?> GetMenuItemById(Guid menuItemId, CancellationToken cancellation);
}

public class GetMenuItemUseCase : IGetMenuItemUseCase
{
    private readonly IMenuRepository _repository;

    public GetMenuItemUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<MenuItemResponse?> GetMenuItemById(Guid menuItemId, CancellationToken cancellation)
    {
        var item = await _repository.GetMenuItemById(menuItemId, cancellation);

        if (item is not null)
        {
            return MenuItemResponse.Convert(item);
        }

        return null;
    }
}