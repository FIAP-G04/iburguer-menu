using iBurguer.Menu.Core.Abstractions;

namespace iBurguer.Menu.Core;

public static class Exceptions
{
    public class InvalidPrice() : DomainException<InvalidPrice>("The price cannot have a value equal to zero or negative");

    public class InvalidCategory() : DomainException<InvalidCategory>("The category informed is not valid");

    public class InvalidUrl() : DomainException<InvalidUrl>("Invalid Url");
    
    public class InvalidTime() : DomainException<InvalidTime>("Preparation time cannot be zero or negative");
    
    public class MaxTime() : DomainException<MaxTime>("Maximum preparation time cannot exceed 120 minutes");
    
    public class MenuItemNotFound() : DomainException<MenuItemNotFound>("No item was found on the menu with the specified ID");
}