using iBurguer.Menu.Core.UseCases.AddMenuItem;
using iBurguer.Menu.Core.UseCases.ChangeMenuItem;
using iBurguer.Menu.Core.UseCases.DisableMenuItem;
using iBurguer.Menu.Core.UseCases.EnableMenuItem;
using iBurguer.Menu.Core.UseCases.GetMenuItem;
using iBurguer.Menu.Core.UseCases.MenuItemsByCategory;
using iBurguer.Menu.Core.UseCases.RemoveMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace iBurguer.Menu.API.Controllers;

/// <summary>
/// API controller for managing menu items.
/// </summary>
[Route("api/menu/items")]
[ApiController]
public class MenuItemController : ControllerBase
{
    /// <summary>
    /// List items by category
    /// </summary>
    /// <remarks>Returns a list of items in the specified category.</remarks>
    /// <param name="useCase">The use case responsible for retrieving categorized menu items.</param>
    /// <param name="category">The category of menu items to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Successful operation. Returns a list of items.</response>
    /// <response code="400">Invalid request. Missing or invalid parameters.</response>
    /// <response code="204">Items not found for the specified category name.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response containing the categorized menu items.</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<MenuItemResponse>), 200)]
    public async Task<ActionResult> GetCategorizedMenuItems([FromServices] IGetCategorizedMenuItemsUseCase useCase, string category, CancellationToken cancellationToken = default)
    {
        var response = await useCase.GetItemsByCategory(category, cancellationToken);

        if (response.Any())
        {
            return Ok(response); 
        }

        return NoContent();
    }

    /// <summary>
    /// Get item by ID
    /// </summary>
    /// <remarks>Returns the menu item with the specified ID.</remarks>
    /// <param name="useCase">The use case responsible for retrieving a menu item.</param>
    /// <param name="id">The ID of the menu item to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Successful operation. Returns the item.</response>
    /// <response code="404">Item not found. The specified item ID does not exist.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response containing the retrieved menu item.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MenuItemResponse), 200)]
    public async Task<ActionResult> GetMenuItemById([FromServices] IGetMenuItemUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.GetMenuItemById(id, cancellationToken);

        if (response is not null)
        {
            return Ok(response);
        }

        return NotFound();
    }
    
    /// <summary>
    /// Create menu item
    /// </summary>
    /// <remarks>Adds a new item to the menu.</remarks>
    /// <param name="useCase">The use case responsible for adding a menu item to the menu.</param>
    /// <param name="request">The request containing information about the menu item to be added.</param>
    /// <response code="201">Item created successfully.</response>
    /// <response code="422">Invalid request. Missing or invalid parameters.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the added menu item.</returns>
    [HttpPost()]
    [ProducesResponseType(typeof(AddMenuItemResponse), 201)]
    public async Task<ActionResult> AddMenuItem([FromServices] IAddMenuItemToMenuUseCase useCase, AddMenuItemRequest request)
    {
        var response = await useCase.AddMenuItem(request);

        return Created("Item created successfully", response);
    }
    
    /// <summary>
    /// Update menu item
    /// </summary>
    /// <remarks>Updates an existing item in the menu.</remarks>
    /// <param name="useCase">The use case responsible for changing the menu item.</param>
    /// <param name="request">The request containing information about the menu item changes.</param>
    /// <response code="200">Item updated successfully.</response>
    /// <response code="422">Invalid request. Missing or invalid parameters.</response>
    /// <response code="404">Item not found. The specified item ID does not exist.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated menu item.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ChangeMenuItemResponse), 200)]
    public async Task<ActionResult> ChangeMenuItem([FromServices] IChangeMenuItemUseCase useCase, ChangeMenuItemRequest request)
    {
        var response = await useCase.ChangeMenuItem(request);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Delete item
    /// </summary>
    /// <remarks>Removes an item from the menu.</remarks>
    /// <param name="useCase">The use case responsible for removing a menu item from the menu.</param>
    /// <param name="id">The ID of the menu item to be removed.</param>
    /// <response code="204">Item deleted successfully.</response>
    /// <response code="404">Item not found. The specified item ID does not exist.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveMenuItem([FromServices] IRemoveItemFromMenuUseCase useCase, Guid id)
    {
        await useCase.RemoveItem(id);
        
        return NoContent(); 
    }
    
    /// <summary>
    /// Enable menu item
    /// </summary>
    /// <remarks>Enables an item in the menu.</remarks>
    /// <param name="useCase">The use case responsible for enabling a menu item.</param>
    /// <param name="id">The ID of the menu item to be enabled.</param>
    /// <response code="200">Item enabled successfully.</response>
    /// <response code="404">Item not found. The specified item ID does not exist.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation.</returns>
    [HttpPatch("{id:guid}/enabled")]
    public async Task<ActionResult> EnableMenuItem([FromServices] IEnableMenuItemUseCase useCase, Guid id)
    {
        await useCase.EnableMenuItem(id);
        
        return Ok(); 
    }
    
    /// <summary>
    /// Disable menu item
    /// </summary>
    /// <remarks>Disables an item in the menu.</remarks>
    /// <param name="useCase">The use case responsible for disabling a menu item.</param>
    /// <param name="id">The ID of the menu item to be disabled.</param>
    /// <response code="200">Item disabled successfully.</response>
    /// <response code="404">Item not found. The specified item ID does not exist.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation.</returns>
    [HttpPatch("{id:guid}/disabled")]
    public async Task<ActionResult> DisableMenuItem([FromServices] IDisableMenuItemUseCase useCase, Guid id)
    {
        await useCase.DisableMenuItem(id);
        
        return Ok(); 
    }
}