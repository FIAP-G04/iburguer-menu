using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.ChangeMenuItem;

public record ChangeMenuItemResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string Category { get; set; }
    public required ushort PreparationTime { get; set; }
    public required string[] ImagesUrl { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public static ChangeMenuItemResponse Convert(Item item)
    {
        return new ChangeMenuItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price.Amount,
            Category = item.Category.ToString(),
            PreparationTime = item.PreparationTime.Minutes,
            ImagesUrl = item.Images.Select(x => x.Value).ToArray(),
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt!.Value
        };
    }
}