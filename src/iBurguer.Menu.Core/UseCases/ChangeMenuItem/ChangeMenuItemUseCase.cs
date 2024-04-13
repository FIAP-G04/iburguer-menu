using iBurguer.Menu.Core.Domain;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.UseCases.ChangeMenuItem;

public interface IChangeMenuItemUseCase
{
    Task<ChangeMenuItemResponse> ChangeMenuItem(ChangeMenuItemRequest request);
}

public class ChangeMenuItemUseCase : IChangeMenuItemUseCase
{
    private readonly IMenuRepository _repository;

    public ChangeMenuItemUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<ChangeMenuItemResponse> ChangeMenuItem(ChangeMenuItemRequest request)
    {
        var item = await _repository.GetMenuItemById(request.Id, CancellationToken.None);
        
        MenuItemNotFound.ThrowIfNull(item);

        item.Update(
            request.Name,
            request.Description,
            request.Price,
            Category.FromName(request.Category),
            request.PreparationTime,
            request.ImagesUrl.Select(url => new Url(url)));

        await _repository.UpdateMenuItem(item);
        
        return ChangeMenuItemResponse.Convert(item);
    }
}