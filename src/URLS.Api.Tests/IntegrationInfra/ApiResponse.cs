using System.Net.Http.Json;
using URLS.Shared;
using URLS.Shared.Api;

namespace URLS.Api.Tests.IntegrationInfra;

public class ApiResponse(HttpResponseMessage responseMessage)
{
    public HttpResponseMessage Response { get; } = responseMessage;
    public string GetBodyAsString() => Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    public ApiResponse<T> GetBody<T>() => Response.Content.ReadFromJsonAsync<ApiResponse<T>>(Settings.Json).GetAwaiter().GetResult()!;
    public int StatusCode => (int)Response.StatusCode;
}
