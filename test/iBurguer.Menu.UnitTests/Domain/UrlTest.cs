using FluentAssertions;
using static iBurguer.Menu.Core.Exceptions;
using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.UnitTests.Domain
{
    public class UrlTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotCreateEmptyUrl(string url)
        {
            var action = () => new Url(url);
            action.Should().Throw<InvalidUrl>();
        }

        [Theory]
        [InlineData("invalid_url")]
        [InlineData("http://")]
        [InlineData("test.")]
        public void ShouldNotCreateInvalidUrl(string url)
        {
            var action = () => new Url(url);
            action.Should().Throw<InvalidUrl>();
        }

        [Theory]
        [InlineData("http://abc.com.br")]
        [InlineData("https://def.com.br")]
        [InlineData("ftp://ghi.com.br")]
        public void ShouldCreateValidUrl(string urlString)
        {
            var url = new Url(urlString);

            url.Should().NotBeNull();
            url.Value.Should().Be(urlString);
        }
    }
}
