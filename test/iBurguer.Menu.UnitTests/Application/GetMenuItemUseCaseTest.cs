using FluentAssertions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.MenuItemsByCategory;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.Application
{
    public class GetMenuItemUseCaseTest
    {
        private readonly IMenuRepository _repository;

        private readonly GetCategorizedMenuItemsUseCase _manipulator;

        public GetMenuItemUseCaseTest()
        {
            _repository = Substitute.For<IMenuRepository>();

            _manipulator = new(_repository);
        }

        [Fact] 
        public async Task ShouldGetItemsByCategory()
        {
            var category = "Drink";
            
            var items = new List<Item>()
            {
                new(
                    "Item name 1", "item description 1",
                    new(10), Category.Drink, 10,
                    new List<Url> {
                        new("http://image.old.com.br")
                    }),
                new(
                    "Item name 2", "item description 2",
                    new(10), Category.Drink, 10,
                    new List<Url> {
                        new("http://image.old.com.br")
                    }),
                new(
                    "Item name 3", "item description 3",
                    new(10), Category.Drink, 10,
                    new List<Url> {
                        new("http://image.old.com.br")
                    }),
            };

            _repository.GetMenuItemsByCategory(Category.FromName(category), Arg.Any<CancellationToken>())
                .Returns(items);

            var result = await _manipulator.GetItemsByCategory(category, default);

            result.Should().NotBeNullOrEmpty();

            foreach(var item in items)
            {
                result
                    .Should()
                    .Contain(r => 
                        r.Id == item.Id && 
                        r.Category == item.Category.ToString() 
                        && r.Category == category);
            }
        }
    }
}
