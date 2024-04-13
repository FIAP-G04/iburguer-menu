using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.UnitTests.Domain
{
    public class PreparationTimeTest
    {
        [Theory]
        [InlineData(0)]
        public void ShouldThrowErrorIfPreparationTimeLessOrEqualThanZero(ushort time)
        {
            var action = () => new PreparationTime(time);

            action.Should().Throw<InvalidTime>();
        }

        [Fact]
        public void ShouldThrowErrorIfPreparationTimeOver120()
        {
            var action = () => new PreparationTime(121);

            action.Should().Throw<InvalidTime>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(120)]
        public void ShouldCreatePreparationTime(ushort time)
        {
            var preparationTime = new PreparationTime(time);

            preparationTime.Should().NotBeNull();
            preparationTime.Minutes.Should().Be(time);
        }
    }
}
