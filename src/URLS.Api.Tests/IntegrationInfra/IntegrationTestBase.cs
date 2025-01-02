namespace URLS.Api.Tests.IntegrationInfra;

[CollectionDefinition(CollectionName, DisableParallelization = true)]
public class NoOpenSearchIntegrationTestFixture : ICollectionFixture<NoOpenSearchTestWebApplicationFactory>
{
    public const string CollectionName = nameof(NoOpenSearchIntegrationTestFixture);
}

public class IntegrationTestBase(ITestOutputHelper output, IntegrationTestWebApplicationFactory factory)
    : IAsyncLifetime
{
    public ITestOutputHelper Output { get; } = output;
    public IntegrationTestWebApplicationFactory Factory { get; } = factory;

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        Factory.ResetTimeProvider();
        await Factory.ResetDbAsync();
    }
}

[Collection(NoOpenSearchIntegrationTestFixture.CollectionName)]
public class NoOpenSearchIntegrationTestBase(ITestOutputHelper output, IntegrationTestWebApplicationFactory factory)
    : IntegrationTestBase(output, factory);