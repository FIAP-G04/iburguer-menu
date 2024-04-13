using iBurguer.Menu.Core.Domain;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.UseCases.RemoveMenuItem;

public interface IRemoveItemFromMenuUseCase
{
    Task RemoveItem(Guid menuItemId);
}

public class RemoveItemFromMenuUseCase : IRemoveItemFromMenuUseCase
{
    private readonly IMenuRepository _repository;

    public RemoveItemFromMenuUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task RemoveItem(Guid menuItemId)
    {
        var item = await _repository.GetMenuItemById(menuItemId, CancellationToken.None);

        MenuItemNotFound.ThrowIfNull(item);
        
        await _repository.RemoveMenuItem(item);
    }

}