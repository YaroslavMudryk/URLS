using FluentAssertions;
using Microsoft.AspNetCore.Http;
using URLS.Api.Tests.IntegrationInfra;

namespace URLS.Api.Tests.Features.Times;

public class DateTimeEndpointsTests(ITestOutputHelper output, NoOpenSearchTestWebApplicationFactory factory)
    : NoOpenSearchIntegrationTestBase(output, factory)
{
    private readonly TimeEndpoints _endpoints = new(factory.Client);

    [Fact]
    public async Task GetServerTime_ShouldReturnCorrectTime()
    {
        var utcNow = DateTime.UtcNow;
        Factory.FakeTimeProvider.SetUtcNow(utcNow);

        var apiResponse = await _endpoints.GetDateTime();

        apiResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
        var actualResponse = apiResponse.GetBodyAsString();
        actualResponse.Should().Be($"\"{utcNow}\"");
    }
}