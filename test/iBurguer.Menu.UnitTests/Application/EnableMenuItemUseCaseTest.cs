using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.EnableMenuItem;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Menu.UnitTests.Application
{
    public class EnableMenuItemUseCaseTest
    {
        private readonly IMenuRepository _repository;

        private readonly EnableMenuItemUseCase _manipulator;

        public EnableMenuItemUseCaseTest()
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

            var action = () => _manipulator.EnableMenuItem(itemId);

            await action.Should().ThrowAsync<MenuItemNotFound>();
        }

        [Fact]
        public async Task ShouldEnableMenuItem()
        {
            var item = new Item(
                "Item name", "item description",
                new(10), Category.Drink, 10,
                new List<Url> {
                    new("http://image.old.com.br")
                });

            item.Disable();

            _repository
                .GetMenuItemById(item.Id, Arg.Any<CancellationToken>())
                .Returns(item);

            await _manipulator.EnableMenuItem(item.Id);

            await _repository
                .Received()
                .UpdateMenuItem(
                    Arg.Is<Item>(x =>
                        x.Id == item.Id &&
                        x.Enabled));
        }
    }
}
