using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.AddMenuItem;

public interface IAddMenuItemToMenuUseCase
{
    Task<AddMenuItemResponse> AddMenuItem(AddMenuItemRequest request);
}

public class AddItemToMenuUseCase : IAddMenuItemToMenuUseCase
{
    private readonly IMenuRepository _repository;

    public AddItemToMenuUseCase(IMenuRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<AddMenuItemResponse> AddMenuItem(AddMenuItemRequest request)
    {
        var item = new Item(
            request.Name,
            request.Description,
            request.Price,
            Category.FromName(request.Category),
            request.PreparationTime,
            request.ImagesUrl.Select(url => new Url(url)));

        await _repository.AddMenuItem(item);

        return AddMenuItemResponse.Convert(item);
    }
}