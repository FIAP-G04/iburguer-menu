using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.MenuItemsByCategory;

public class MenuItemResponse()
{
    /// <summary>
    /// The unique identifier of the menu item.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// The name of the menu item.
    /// </summary>
    /// <example>Beef Burguer</example>
    public required string Name { get; set; }
    
    /// <summary>
    /// A brief description of the menu item.
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// The price of the menu item.
    /// </summary>
    public required decimal Price { get; set; }
    
    /// <summary>
    /// The category to which the menu item belongs (e.g., MainDish, SideDish, Dessert, Drink).
    /// </summary>
    public required string Category { get; set; }
    
    /// <summary>
    /// The estimated preparation time for the menu item in minutes.
    /// </summary>
    public required ushort PreparationTime { get; set; }
    
    /// <summary>
    /// The URLs of images representing the menu item.
    /// </summary>
    public required string[] ImagesUrl { get; set; }

    public static MenuItemResponse Convert(Item item)
    {
        return new MenuItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price.Amount,
            Category = item.Category.ToString(),
            PreparationTime = item.PreparationTime.Minutes,
            ImagesUrl = item.Images.Select(x => x.Value).ToArray()
        };
    }
}

