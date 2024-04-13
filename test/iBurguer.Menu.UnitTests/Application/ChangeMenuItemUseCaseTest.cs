using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.ChangeMenuItem;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Menu.UnitTests.Application
{
    public class ChangeMenuItemUseCaseTest
    {
        private readonly IMenuRepository _repository;

        private readonly ChangeMenuItemUseCase _manipulator;

        public ChangeMenuItemUseCaseTest()
        {
            _repository = Substitute.For<IMenuRepository>();

            _manipulator = new(_repository);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenItemNotFound()
        {
            var request = new ChangeMenuItemRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Item",
                Description = "Item description",
                Price = 10,
                Category = "MainDish",
                PreparationTime = 10,
                ImagesUrl = new string[] { "http://image.com.br" }
            };

            _repository.GetMenuItemById(request.Id, Arg.Any<CancellationToken>()).ReturnsNull();

            var action = () => _manipulator.ChangeMenuItem(request);

            await action.Should().ThrowAsync<MenuItemNotFound>();
        }

        [Fact]
        public async Task ShouldUpdateMenuItem()
        {
            var item = new Item(
                "Item name", "Old item description", 
                new(11), Category.Drink, 11, 
                new List<Url> { 
                    new("http://image.old.com.br") 
                } );

            var request = new ChangeMenuItemRequest()
            {
                Id = item.Id,
                Name = "Item",
                Description = "Item description",
                Price = 10,
                Category = "MainDish",
                PreparationTime = 10,
                ImagesUrl = new string[] { "http://image.com.br" }
            };

            _repository.GetMenuItemById(item.Id, Arg.Any<CancellationToken>()).Returns(item);

            var result = await _manipulator.ChangeMenuItem(request);

            result.Should().NotBeNull();
            result.Id.Should().Be(item.Id.Value);
            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.Price.Should().Be(request.Price);
            result.Category.Should().Be(request.Category);
            result.PreparationTime.Should().Be(request.PreparationTime);
            result.ImagesUrl.Should().BeEquivalentTo(request.ImagesUrl);

            await _repository.Received()
                .UpdateMenuItem(
                    Arg.Is<Item>(x => 
                        x.Id == item.Id &&
                        x.Name == request.Name &&
                        x.Description == request.Description &&
                        x.Price.Amount == request.Price &&
                        x.Category.ToString() == request.Category &&
                        x.PreparationTime.Minutes == request.PreparationTime &&
                        x.Images.All(i => request.ImagesUrl.ToList().Contains(i))));

        }
    }
}
