using AutoFixture.Xunit2;
using FluentAssertions;
using iBurguer.Menu.API.Controllers;
using iBurguer.Menu.Core.UseCases.Categories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.API;

public class CategoryControllerTest
{
    public readonly CategoryController _sut;

    public CategoryControllerTest()
    {
        _sut = new CategoryController();
    }

    [Theory, AutoData]
    public void GetCategories_ShouldReturnOk_WhenCategoriesFound(IEnumerable<string> categories)
    {
        // Arrange
        var useCase = Substitute.For<IGetCategoriesUseCase>();
        useCase.GetCategories().Returns(categories);

        // Act
        var result = _sut.GetCategories(useCase);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(categories);
    }

    [Fact]
    public void GetCategories_ShouldReturnNoContent_WhenNoCategoriesFound()
    {
        // Arrange
        var useCase = Substitute.For<IGetCategoriesUseCase>();
        useCase.GetCategories().Returns(Enumerable.Empty<string>());

        // Act
        var result = _sut.GetCategories(useCase);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}

