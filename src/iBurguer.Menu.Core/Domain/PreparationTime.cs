using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.Domain;

public sealed record PreparationTime
{
    public PreparationTime(ushort time)
    {
        InvalidTime.ThrowIf(time <= 0);
        InvalidTime.ThrowIf(time > 120);

        Minutes = time;
    }

    public ushort Minutes { get; }

    public override string ToString() => Minutes.ToString();

    public static implicit operator ushort(PreparationTime time) => time.Minutes;

    public static implicit operator PreparationTime(ushort minutes) => new(minutes);
}