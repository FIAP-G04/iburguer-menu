using FluentAssertions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.AddMenuItem;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.Application
{
    public class AddItemToMenuUseCaseTest
    {
        private readonly IMenuRepository _repository = Substitute.For<IMenuRepository>();

        private readonly AddItemToMenuUseCase _manipulator;

        public AddItemToMenuUseCaseTest()
        {
            _manipulator = new(_repository);
        }

        [Fact]
        public async Task ShouldAddItemToMenu()
        {
            var request = new AddMenuItemRequest()
            {
                Name = "ProductName",
                Description = "Product Description",
                Price = 10.10M,
                Category = "MainDish",
                PreparationTime = 10,
                ImagesUrl = new string[] { "http://image.url.com" }
            };

            var result = await _manipulator.AddMenuItem(request);

            await _repository
                .Received()
                .AddMenuItem(Arg.Is<Item>(x =>
                    x.Name == request.Name &&
                    x.Description == request.Description &&
                    x.Price.Amount == request.Price &&
                    x.Category.ToString() == request.Category &&
                    x.PreparationTime.Minutes == request.PreparationTime &&
                    x.Images.All(i => request.ImagesUrl.ToList().Contains(i))));

            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.Price.Should().Be(request.Price);
            result.Category.Should().Be(request.Category);
            result.PreparationTime.Should().Be(request.PreparationTime);
            result.ImagesUrl.Should().BeEquivalentTo(request.ImagesUrl);
        }
    }
}
