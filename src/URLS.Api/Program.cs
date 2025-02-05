using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using System.Globalization;
using URLS.Api.Infrastructure.Middlewares;
using URLS.Api.Logging;
using URLS.Data;
using URLS.Data.Audit;
using URLS.Groups;
using URLS.Identity;
using URLS.LearningProcess;
using URLS.Notifications;
using URLS.Organization;
using URLS.Reviews;
using URLS.Shared;
using URLS.Shared.Auth;
using URLS.Testing;
using URLS.University;

namespace URLS.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

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
        app.UseMiddleware<CorrelationContextMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseMiddleware<AuthenticationMiddleware>();
        app.UseHttpsRedirection();

        MapFeatureEndpoints(app);

        app.Run();
    }

    private static void MapFeatureEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/server-time", (TimeProvider timeProvider) =>
        {
            var currentTime = timeProvider.GetUtcNow().UtcDateTime.ToString(CultureInfo.InvariantCulture);
            return Results.Ok(new { time = currentTime });
        });

        IdentityEndpoints.Map(app);
        GroupsEndpoints.Map(app);
        LearningProcessEndpoints.Map(app);
        NotificationsEndpoints.Map(app);
        OrganizationEndpoints.Map(app);
        ReviewsEndpoints.Map(app);
        TestingEndpoints.Map(app);
        UniversityEndpoints.Map(app);
    }

    private static void RegisterFeatureDependencies(IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<AuditRepo>();
        services.AddScoped<ITokenResolverService, MockTokenResolverService>();

        IdentityDependencies.Register(services);
        GroupsDependencies.Register(services);
        LearningProcessDependencies.Register(services);
        NotificationsDependencies.Register(services);
        OrganizationDependencies.Register(services);
        ReviewsDependencies.Register(services);
        TestingDependencies.Register(services);
        UniversityDependencies.Register(services);
    }

    private static void RegisterDbDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UrlsContext>(o =>
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
        services.AddTransient<CorrelationContextMiddleware>();
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        services.AddTransient<LoggingMiddleware>();
        services.AddTransient<ETagMiddleware>();
        services.AddTransient<AuthenticationMiddleware>();
    }
}
