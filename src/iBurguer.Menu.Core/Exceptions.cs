using iBurguer.Menu.Core.Abstractions;

namespace iBurguer.Menu.Core;

public static class Exceptions
{
    public class InvalidPrice() : DomainException<InvalidPrice>("O preço não pode ter valor igual a zero ou negativo");

    public class InvalidCategory() : DomainException<InvalidCategory>("A categoria informada não é válida");

    public class InvalidUrl() : DomainException<InvalidUrl>("Url inválida");
    
    public class InvalidTime() : DomainException<InvalidTime>("O tempo de preparação não pode ser igual a zero ou negativo");
    
    public class MaxTime() : DomainException<MaxTime>("O tempo máximo de preparação não pode ultrapassar 120 minutos");
    
    public class MenuItemNotFound() : DomainException<MenuItemNotFound>($"Não foi encontrado nenhum item no cardápio com o Id informado");
}