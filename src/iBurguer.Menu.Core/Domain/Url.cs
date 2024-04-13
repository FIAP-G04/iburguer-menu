using System.Text.RegularExpressions;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.Domain;

public record Url
{
    private const string urlPattern = @"^(http|https|ftp)://[A-Za-z0-9.-]+(/[A-Za-z0-9/_.-]+)*$";

    public string Value { get; }

    public Url(string url)
    {
        InvalidUrl.ThrowIf(string.IsNullOrEmpty(url));
        InvalidUrl.ThrowIf(!Regex.IsMatch(url, urlPattern));

        Value = url;
    }

    public override string ToString() => Value;

    public static implicit operator string(Url url) => url.Value;

    public static implicit operator Url(string url) => new(url);
}