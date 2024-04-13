using FluentValidation;

namespace iBurguer.Menu.Core.UseCases.AddMenuItem;

public class AddMenuItemRequest
{
    /// <summary>
    /// The name of the menu item.
    /// </summary>
    /// <example>Beef Burguer</example>
    public string Name { get; set; }
    
    /// <summary>
    /// A brief description of the menu item.
    /// </summary>
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public ushort PreparationTime { get; set; }
    public string[] ImagesUrl { get; set; }
    
    public class Validator : AbstractValidator<AddMenuItemRequest>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.Category).NotEmpty();
            RuleFor(r => r.PreparationTime).NotEmpty().GreaterThan((ushort)0).LessThanOrEqualTo((ushort)120);
        }
    }
}