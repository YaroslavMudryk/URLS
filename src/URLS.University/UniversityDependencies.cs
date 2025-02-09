using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using URLS.University.Features.Universities.DataAccess;
using URLS.University.Features.Universities.Dtos;
using URLS.University.Features.Universities.Dtos.Validators;
using URLS.University.Features.Universities.Services;

namespace URLS.University;

public static class UniversityDependencies
{
    public static void Register(IServiceCollection services)
    {
        //Orchestrators
        services.AddScoped<GetUniversityOrchestrator>();
        services.AddScoped<CreateUniversityOrchestrator>();
        services.AddScoped<UpdateUniversityOrchestrator>();

        //DataAccess
        services.AddScoped<UniversityRepo>();
        services.AddScoped<UniversityQuery>();

        //Services
        services.AddScoped<UniversityService>();

        // Validators
        services.AddScoped<IValidator<CreateUniversityRequest>, CreateUniversityRequestValidator>();
    }
}
