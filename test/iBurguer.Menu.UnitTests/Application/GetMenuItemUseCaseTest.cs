using FluentAssertions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.GetMenuItem;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.Application;

public class GetMenuItemUseCaseTest
{
    private readonly IMenuRepository _repository;

    private readonly GetMenuItemUseCase _manipulator;

    public GetMenuItemUseCaseTest()
    {
        _repository = Substitute.For<IMenuRepository>();

        _manipulator = new(_repository);
    }

    [Fact]
    public async Task ShouldGetItemsById()
    {
        var id = Guid.NewGuid();

        var item = new Item("Item name 1", "item description 1",
                new(10), Category.Drink, 10,
                [
                    new("http://image.old.com.br")
                ]);

        _repository.GetMenuItemById(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var result = await _manipulator.GetMenuItemById(id, default);

        result.Should().NotBeNull();
        result.Category.Should().Be(item.Category.ToString());
        result.Description.Should().Be(item.Description);
        result.Name.Should().Be(item.Name);
        result.PreparationTime.Should().Be(item.PreparationTime);
        result.Price.Should().Be(item.Price);

    }

}
