using iBurguer.Menu.Core.Abstractions;

namespace iBurguer.Menu.Core.Domain;

public record MenuItemPriceUpdated(Id ProductId, Price NewPrice, Price OldPrice) : IDomainEvent;