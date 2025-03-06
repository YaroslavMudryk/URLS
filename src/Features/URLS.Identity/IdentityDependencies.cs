using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Identity.Features.Apps.Dtos.Validators;
using URLS.Identity.Features.Apps.Orchestrators;
using URLS.Identity.Features.Apps.Services;
using URLS.Identity.Features.Claims.Orchestrators;
using URLS.Identity.Features.Roles.Orchestrators;
using URLS.Identity.Features.Users.Orchestrators;

namespace URLS.Identity;

public static class IdentityDependencies
{
    public static void Register(IServiceCollection services)
    {
        //Orchestrators
        services.AddScoped<CreateAppOrchestrator>();
        services.AddScoped<DeleteAppOrchestrator>();
        services.AddScoped<GetAppOrchestrator>();
        services.AddScoped<GetAppSecretsOrchestrator>();
        services.AddScoped<GetAppsOrchestrator>();
        services.AddScoped<UpdateAppOrchestrator>();

        services.AddScoped<GetClaimsOrchestrator>();
        services.AddScoped<UpdateClaimOrchestrator>();
        
        services.AddScoped<CreateRoleOrchestrator>();
        services.AddScoped<DeleteRoleOrchestrator>();
        services.AddScoped<GetRoleOrchestrator>();
        services.AddScoped<GetRolesOrchestrator>();
        services.AddScoped<UpdateRoleOrchestrator>();

        services.AddScoped<DeleteSessionOrchestrator>();
        services.AddScoped<DeleteSessionsOrchestrator>();
        services.AddScoped<GetSessionsOrchestrator>();
        services.AddScoped<GetUserByIdOrchestrator>();
        services.AddScoped<GetUserOrchestrator>();
        services.AddScoped<RefreshTokenOrchestrator>();
        services.AddScoped<SearchUsersOrchestrator>();
        services.AddScoped<SignInOrchestrator>();
        services.AddScoped<SignOutOrchestrator>();
        services.AddScoped<SignUpOrchestrator>();

        //DataAccess
        services.AddScoped<IAppsQuery, AppsQuery>();
        services.AddScoped<AppRepo>();

        //Services
        services.AddScoped<AppsEnricherService>();
        services.AddScoped<AppService>();
        services.AddScoped<AppClaimsService>();

        //Validators
        services.AddScoped<IValidator<CreateAppRequest>, CreateAppRequestValidator>();
        services.AddScoped<IValidator<UpdateAppRequest>, UpdateAppRequestValidator>();
    }
}
