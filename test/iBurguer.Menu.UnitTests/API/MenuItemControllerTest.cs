using AutoFixture.Xunit2;
using FluentAssertions;
using iBurguer.Menu.API.Controllers;
using iBurguer.Menu.Core.UseCases.AddMenuItem;
using iBurguer.Menu.Core.UseCases.ChangeMenuItem;
using iBurguer.Menu.Core.UseCases.DisableMenuItem;
using iBurguer.Menu.Core.UseCases.EnableMenuItem;
using iBurguer.Menu.Core.UseCases.GetMenuItem;
using iBurguer.Menu.Core.UseCases.MenuItemsByCategory;
using iBurguer.Menu.Core.UseCases.RemoveMenuItem;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace iBurguer.Menu.UnitTests.API;

public class MenuItemControllerTest
{
    public readonly MenuItemController _sut;

    public MenuItemControllerTest()
    {
        _sut = new MenuItemController();
    }

    [Theory, AutoData]
    public async Task GetCategorizedMenuItems_ShouldReturnOk_WhenItemsFound(string category, IEnumerable<MenuItemResponse> menuItems)
    {
        // Arrange
        var useCase = Substitute.For<IGetCategorizedMenuItemsUseCase>();
        useCase.GetItemsByCategory(category, Arg.Any<CancellationToken>()).Returns(menuItems);

        // Act
        var result = await _sut.GetCategorizedMenuItems(useCase, category);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(menuItems);
    }

    [Theory, AutoData]
    public async Task GetCategorizedMenuItems_ShouldReturnNoContent_WhenNoItemsFound(string category)
    {
        // Arrange
        var useCase = Substitute.For<IGetCategorizedMenuItemsUseCase>();
        useCase.GetItemsByCategory(category, Arg.Any<CancellationToken>()).Returns(Enumerable.Empty<MenuItemResponse>());

        // Act
        var result = await _sut.GetCategorizedMenuItems(useCase, category);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Theory, AutoData]
    public async Task GetMenuItemById_ShouldReturnOk_WhenItemFound(Guid id, MenuItemResponse menuItem)
    {
        // Arrange
        var useCase = Substitute.For<IGetMenuItemUseCase>();
        useCase.GetMenuItemById(id, Arg.Any<CancellationToken>()).Returns(menuItem);

        // Act
        var result = await _sut.GetMenuItemById(useCase, id);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(menuItem);
    }

    [Theory, AutoData]
    public async Task GetMenuItemById_ShouldReturnNotFound_WhenItemNotFound(Guid id)
    {
        // Arrange
        var useCase = Substitute.For<IGetMenuItemUseCase>();
        useCase.GetMenuItemById(id, Arg.Any<CancellationToken>()).Returns((MenuItemResponse)null);

        // Act
        var result = await _sut.GetMenuItemById(useCase, id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory, AutoData]
    public async Task AddMenuItem_ShouldReturnCreated_WhenItemAdded(AddMenuItemRequest request, AddMenuItemResponse response)
    {
        // Arrange
        var useCase = Substitute.For<IAddMenuItemToMenuUseCase>();
        useCase.AddMenuItem(request).Returns(response);

        // Act
        var result = await _sut.AddMenuItem(useCase, request);

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().BeEquivalentTo(response);
    }

    [Theory, AutoData]
    public async Task ChangeMenuItem_ShouldReturnOk_WhenItemUpdated(ChangeMenuItemRequest request, ChangeMenuItemResponse response)
    {
        // Arrange
        var useCase = Substitute.For<IChangeMenuItemUseCase>();
        useCase.ChangeMenuItem(request).Returns(response);

        // Act
        var result = await _sut.ChangeMenuItem(useCase, request);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(response);
    }

    [Theory, AutoData]
    public async Task RemoveMenuItem_ShouldReturnNoContent_WhenItemRemoved(Guid id)
    {
        // Arrange
        var useCase = Substitute.For<IRemoveItemFromMenuUseCase>();

        // Act
        var result = await _sut.RemoveMenuItem(useCase, id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Theory, AutoData]
    public async Task EnableMenuItem_ShouldReturnOk_WhenItemEnabled(Guid id)
    {
        // Arrange
        var useCase = Substitute.For<IEnableMenuItemUseCase>();

        // Act
        var result = await _sut.EnableMenuItem(useCase, id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Theory, AutoData]
    public async Task DisableMenuItem_ShouldReturnOk_WhenItemDisabled(Guid id)
    {
        // Arrange
        var useCase = Substitute.For<IDisableMenuItemUseCase>();

        // Act
        var result = await _sut.DisableMenuItem(useCase, id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}

