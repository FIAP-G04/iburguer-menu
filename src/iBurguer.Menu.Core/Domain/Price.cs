using System.Globalization;
using static iBurguer.Menu.Core.Exceptions; 

namespace iBurguer.Menu.Core.Domain;

public sealed record Price
{
    public decimal Amount { get; }

    public Price(decimal amount)
    {
        InvalidPrice.ThrowIf(amount <= 0);

        Amount = amount;
    }

    public override string ToString() => Amount.ToString(CultureInfo.InvariantCulture);

    public static implicit operator decimal(Price price) => price.Amount;

    public static implicit operator Price(decimal value) => new(value);
}