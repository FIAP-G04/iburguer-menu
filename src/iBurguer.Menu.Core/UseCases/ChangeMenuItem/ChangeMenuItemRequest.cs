using FluentValidation;

namespace iBurguer.Menu.Core.UseCases.ChangeMenuItem;

public class ChangeMenuItemRequest 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public ushort PreparationTime { get; set; }
    public string[] ImagesUrl { get; set; }
    
    public class Validator : AbstractValidator<ChangeMenuItemRequest>
    {
        public Validator()
        {
            RuleFor(r => r.Id).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.Category).NotEmpty();
            RuleFor(r => r.PreparationTime).NotEmpty().GreaterThan((ushort)0).LessThanOrEqualTo((ushort)120);
        }
    }
}