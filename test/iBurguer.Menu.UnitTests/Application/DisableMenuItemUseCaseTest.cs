using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.DisableMenuItem;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Menu.UnitTests.Application
{
    public class DisableMenuItemUseCaseTest
    {
        private readonly IMenuRepository _repository;

        private readonly DisableMenuItemUseCase _manipulator;

        public DisableMenuItemUseCaseTest()
        {
            _repository = Substitute.For<IMenuRepository>();

            _manipulator = new(_repository);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenItemNotFound()
        {
            var itemId = Guid.NewGuid();

            _repository
                .GetMenuItemById(itemId, Arg.Any<CancellationToken>())
                .ReturnsNull();

            var action = () => _manipulator.DisableMenuItem(itemId);

            await action.Should().ThrowAsync<MenuItemNotFound>();
        }

        [Fact]
        public async Task ShouldDisableMenuItem()
        {
            var item = new Item(
                "Item name", "item description",
                new(10), Category.Drink, 10,
                new List<Url> {
                    new("http://image.old.com.br")
                });

            _repository
                .GetMenuItemById(item.Id, Arg.Any<CancellationToken>())
                .Returns(item);

            await _manipulator.DisableMenuItem(item.Id);

            await _repository
                .Received()
                .UpdateMenuItem(
                    Arg.Is<Item>(x => 
                        x.Id == item.Id &&
                        !x.Enabled));
        }
    }
}
