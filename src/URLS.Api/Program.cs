using Microsoft.EntityFrameworkCore;
using Serilog;
using URLS.Api.Infrastructure.Middlewares;
using URLS.Api.Logging;
using URLS.Data;
using URLS.Shared;

namespace URLS.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, configuration) =>
        configuration
            .ReadFrom.Configuration(builder.Configuration)
            .Destructure.With<ExcludeNullPropertiesPolicy>()
            .Enrich.FromLogContext()
            .Enrich.With<RemovePropertiesEnricher>()
            .Enrich.With<LogLevelEnricher>()
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName));

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        RegisterSystemDependencies(builder.Services);
        RegisterFeatureDependencies(builder.Services);
        RegisterDbDependencies(builder);
        RegisterMiddlewares(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ETagMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        MapFeatureEndpoints(app);

        app.Run();
    }

    private static void MapFeatureEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/server-time", (TimeProvider timeProvider) =>
            {
                return Results.Ok(timeProvider.GetUtcNow().UtcDateTime.ToString());
            });
    }

    private static void RegisterFeatureDependencies(IServiceCollection builderServices)
    {

    }

    private static void RegisterDbDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UrlsContext>(o =>
        {
            o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
        });
    }

    private static void RegisterSystemDependencies(IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = Settings.Json.PropertyNamingPolicy;
            options.SerializerOptions.DictionaryKeyPolicy = Settings.Json.PropertyNamingPolicy;
        });

        services.AddSingleton(TimeProvider.System);
    }

    private static void RegisterMiddlewares(IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        services.AddTransient<LoggingMiddleware>();
        services.AddTransient<ETagMiddleware>();
    }
}
