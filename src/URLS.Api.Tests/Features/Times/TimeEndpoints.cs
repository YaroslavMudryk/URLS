using URLS.Api.Tests.IntegrationInfra;

namespace URLS.Api.Tests.Features.Times;

public class TimeEndpoints(HttpClient client)
{
    public Task<ApiResponse> GetDateTime()
        => ApiRequest.Get("/api/v1/server-time").SendAsync(client);
}
