using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Core.UseCases.Categories;
using iBurguer.Menu.Infrastructure.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace iBurguer.Menu.API.Controllers;

/// <summary>
/// API controller for retrieving menu categories.
/// </summary>
[Route("api/menu/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    /// /// <summary>
    /// Retrieves all menu categories.
    /// </summary>
    /// <param name="useCase">The use case responsible for retrieving menu categories.</param>
    /// <response code="200">Returns a list of categories.</response>
    /// <response code="204">No categories found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response containing the list of categories.</returns>
    [HttpGet]
    public ActionResult GetCategories([FromServices] IGetCategoriesUseCase useCase)
    {
        var response = useCase.GetCategories();

        if (response.Any())
        {
            return Ok(response); 
        }
        
        return NoContent();
    }
}