using Microsoft.AspNetCore.WebUtilities;

namespace MyFi.TheBadlands.Extensions;

public static class StringExtensions
{
    public static string AddQueryParameters(this string uri, string name, object value)
        => QueryHelpers.AddQueryString(uri, name, value.ToString() ?? string.Empty);

}