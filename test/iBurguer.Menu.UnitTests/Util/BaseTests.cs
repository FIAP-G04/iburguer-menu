using AutoFixture;

namespace iBurguer.Menu.UnitTests.Util;

public class BaseTests
{
    private readonly Fixture _fixture;

    public BaseTests()
    {
        _fixture = new Fixture();
    }

    public Fixture Fake => _fixture;
}