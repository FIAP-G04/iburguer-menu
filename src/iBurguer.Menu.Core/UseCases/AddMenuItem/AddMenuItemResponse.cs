using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.AddMenuItem;

public record AddMenuItemResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string Category { get; set; }
    public required ushort PreparationTime { get; set; }
    public required string[] ImagesUrl { get; set; }
    public required DateTime CreatedAt { get; set; }

    public static AddMenuItemResponse Convert(Item item)
    {
        return new AddMenuItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price.Amount,
            Category = item.Category.ToString(),
            PreparationTime = item.PreparationTime.Minutes,
            ImagesUrl = item.Images.Select(x => x.Value).ToArray(),
            CreatedAt = item.CreatedAt
        };
    }
}