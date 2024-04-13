using FluentAssertions;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.Categories;

namespace iBurguer.Menu.UnitTests.Application
{
    public class GetCategoriesUseCaseTest
    {
        private readonly GetCategoriesUseCase _manipulator;

        public GetCategoriesUseCaseTest()
        {
            _manipulator = new GetCategoriesUseCase();
        }

        [Fact]
        public void ShouldReturnCategories()
        {
            var categories = _manipulator.GetCategories();

            categories.Should().NotBeNullOrEmpty();
            categories.Should().Contain(Category.MainDish.ToString());
            categories.Should().Contain(Category.SideDish.ToString());
            categories.Should().Contain(Category.Drink.ToString());
            categories.Should().Contain(Category.Dessert.ToString());
        }
    }
}
