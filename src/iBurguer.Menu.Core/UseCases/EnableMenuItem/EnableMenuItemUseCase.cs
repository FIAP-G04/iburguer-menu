using iBurguer.Menu.Core.Domain;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.UseCases.EnableMenuItem;

public interface IEnableMenuItemUseCase
{
    Task EnableMenuItem(Guid menuItemId);
}

public class EnableMenuItemUseCase : IEnableMenuItemUseCase
{
    private readonly IMenuRepository _repository;

    public EnableMenuItemUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task EnableMenuItem(Guid menuItemId)
    {
        var item = await _repository.GetMenuItemById(menuItemId, CancellationToken.None);

        MenuItemNotFound.ThrowIfNull(item);

        item!.Enable();

        await _repository.UpdateMenuItem(item);
    }
}