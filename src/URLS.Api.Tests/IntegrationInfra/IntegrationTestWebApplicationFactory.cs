using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using URLS.Data;
using URLS.WebApi;

namespace URLS.Api.Tests.IntegrationInfra;

public abstract class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    protected TestDatabase TestDatabase { init; get; } = new();

    public HttpClient Client { get; private set; } = default!;

    public string By { get; private set; } = default;

    public FakeTimeProvider FakeTimeProvider { get; private set; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureTestServices(services =>
        {
            services.Replace<TimeProvider>(s => s.AddTransient<TimeProvider>(_ => FakeTimeProvider));
        });

        By = "1";

        TestDatabase.ConfigureTestServices(builder);
    }

    public virtual Task InitializeAsync()
    {
        Client = CreateClient();
        return Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await TestDatabase.DisposeAsync();
    }

    public void ResetTimeProvider() => FakeTimeProvider = new();

    public UrlsContext CreateDbContext() => TestDatabase.CreateDbContext(Services.CreateScope());

    public Task ResetDbAsync() => TestDatabase.ResetDbAsync();
}

public class NoOpenSearchTestWebApplicationFactory : IntegrationTestWebApplicationFactory
{
    public override async Task InitializeAsync()
    {
        await using var scope = Services.CreateAsyncScope();
        await TestDatabase.InitializeAsync(scope);

        await base.InitializeAsync();
    }
}