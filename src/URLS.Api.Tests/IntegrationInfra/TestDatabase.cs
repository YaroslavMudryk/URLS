using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using URLS.Data;

namespace URLS.Api.Tests.IntegrationInfra;

public sealed class TestDatabase : IAsyncDisposable
{
    private Respawner _respawner = default!;
    private NpgsqlConnection _npgsqlConnection = default!;

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

    private async Task AddDbResetter()
    {
        _respawner = await Respawner.CreateAsync(_npgsqlConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await DisposeAsync();
    }

    public async Task EnsureDbIsCreated(IServiceScope scope)
    {
        await using var dbContext = CreateDbContext(scope);
        await dbContext.Database.EnsureCreatedAsync();
    }

    public Task ResetDbAsync() => _respawner.ResetAsync(_npgsqlConnection);

    public async Task InitializeAsync(IServiceScope scope)
    {
        await _postgreSqlContainer.StartAsync();
        await EnsureDbIsCreated(scope);

        _npgsqlConnection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await _npgsqlConnection.OpenAsync();

        await AddDbResetter();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
        await _npgsqlConnection.DisposeAsync();
    }

    public UrlsContext CreateDbContext(IServiceScope scope) => scope?.ServiceProvider.GetRequiredService<UrlsContext>();

    public UrlsContext CreateOriginalDbContext(IServiceScope scope)
    {
        return scope?.ServiceProvider.GetRequiredService<UrlsContext>();
    }

    public void ConfigureTestServices(IWebHostBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.ConfigureTestServices(serviceCollection => serviceCollection
            .Remove<DbContextOptions<UrlsContext>>()
            .Replace<UrlsContext>(s =>
                s.AddDbContext<UrlsContext>(o
                    => o.UseNpgsql(_postgreSqlContainer.GetConnectionString()))
            ));

        builder.ConfigureTestServices(serviceCollection => serviceCollection.AddDbContext<UrlsContext>(o =>
                o.UseNpgsql(_postgreSqlContainer.GetConnectionString())));
    }
}
