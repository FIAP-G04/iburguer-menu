using FluentAssertions;
using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.UnitTests.Domain
{
    public class CategoryTest
    {
        [Fact]
        public void ShouldRetrieveCategoryFromName()
        {
            Category.FromName("MainDish").Should().Be(Category.MainDish);
            Category.FromName("SideDish").Should().Be(Category.SideDish);
            Category.FromName("Drink").Should().Be(Category.Drink);
            Category.FromName("Dessert").Should().Be(Category.Dessert);
        }

        [Fact]
        public void ShouldRetrieveCategoryFromId()
        {
            Category.FromId(1).Should().Be(Category.MainDish);
            Category.FromId(2).Should().Be(Category.SideDish);
            Category.FromId(3).Should().Be(Category.Drink);
            Category.FromId(4).Should().Be(Category.Dessert);
        }

        [Fact]
        public void ShouldListCategories()
        {
            var list = Category.ToList();

            list.Should().Contain("MainDish");
            list.Should().Contain("SideDish");
            list.Should().Contain("Drink");
            list.Should().Contain("Dessert");
        }
    }
}
