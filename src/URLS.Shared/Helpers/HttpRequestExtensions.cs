using Microsoft.AspNetCore.Http;

namespace URLS.Shared.Helpers;

public static class HttpRequestExtensions
{
    public static string GetValueOrNull(this IHeaderDictionary headers, string key)
    {
        ArgumentNullException.ThrowIfNull(headers);

        return headers[key].FirstOrDefault();
    }
}
