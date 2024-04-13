using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.UnitTests.Domain
{
    public class PriceTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldThrowErrorWhenPriceIsLessOrEqualThanZero(decimal amount)
        {
            var action = () => new Price(amount);

            action.Should().Throw<InvalidPrice>();
        }

        [Fact]
        public void ShouldCreatePrice()
        {
            var price = new Price(10);

            price.Should().NotBeNull();
            price.Amount.Should().Be(10);
        }
    }
}
