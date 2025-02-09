using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using URLS.University.Features.Faculties.DataAccess;
using URLS.University.Features.Faculties.Dtos;
using URLS.University.Features.Faculties.Dtos.Validators;
using URLS.University.Features.Faculties.Services;
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
        services.AddScoped<GetFacultiesOrchestrator>();
        services.AddScoped<CreateFacultyOrchestrator>();
        services.AddScoped<UpdateFacultyOrchestrator>();
        services.AddScoped<DeleteFacultyOrchestrator>();

        //DataAccess
        services.AddScoped<UniversityRepo>();
        services.AddScoped<UniversityQuery>();
        services.AddScoped<FacultiesRepo>();
        services.AddScoped<IFacultiesQuery, FacultiesQuery>();

        // Validators
        services.AddScoped<IValidator<CreateUniversityRequest>, CreateUniversityRequestValidator>();
        services.AddScoped<IValidator<CreateFacultyRequest>, CreateFacultyRequestValidator>();
    }
}
