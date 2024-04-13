using iBurguer.Menu.Core.Domain;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.UseCases.DisableMenuItem;

public interface IDisableMenuItemUseCase
{
    Task DisableMenuItem(Guid menuItemId);
}

public class DisableMenuItemUseCase : IDisableMenuItemUseCase
{
    private readonly IMenuRepository _repository;

    public DisableMenuItemUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task DisableMenuItem(Guid menuItemId)
    {
        var item = await _repository.GetMenuItemById(menuItemId, CancellationToken.None);

        MenuItemNotFound.ThrowIfNull(item);

        item!.Disable();

        await _repository.UpdateMenuItem(item);
    }
}