using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using URLS.Shared.Api;
using URLS.University.Features.Faculties.Dtos;
using URLS.University.Features.Faculties.Services;
using URLS.University.Features.Specialties.Dtos;
using URLS.University.Features.Specialties.Services;
using URLS.University.Features.Universities.Dtos;
using URLS.University.Features.Universities.Services;

namespace URLS.University;

public static class UniversityEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/v1/universities",
            async (GetUniversityOrchestrator orc) =>
            {
                var university = await orc.GetUniversityAsync();
                return Results.Ok(university.MapToResponse());
            })
            .Produces<ApiResponse<UniversityDto>>();

        app.MapPost("/api/v1/universities",
            async (CreateUniversityRequest request, CreateUniversityOrchestrator orc) =>
            {
                var createdUniversity = await orc.CreateUniversityAsync(request);
                return Results.Json(createdUniversity.MapToResponse(), statusCode: 201);
            })
            .Produces<ApiResponse<UniversityDto>>();

        app.MapPut("/api/v1/universities/{universityId:int}",
            async (int universityId, CreateUniversityRequest request, UpdateUniversityOrchestrator orc) =>
            {
                var updatedUniversity = await orc.UpdateUniversityAsync(universityId, request);
                return Results.Ok(updatedUniversity.MapToResponse());
            })
            .Produces<ApiResponse<UniversityDto>>();

        app.MapGet("/api/v1/universities/{universityId:int}/faculties",
            async (int universityId, GetFacultiesOrchestrator orc) =>
            {
                var faculties = await orc.GetFacultiesAsync(universityId);
                return Results.Ok(faculties.MapToResponse());
            })
            .Produces<ApiResponse<FacultiesResponse>>();

        app.MapPost("/api/v1/faculties",
            async (CreateFacultyRequest request, CreateFacultyOrchestrator orc) =>
            {
                var createdFaculty = await orc.CreateFacultyAsync(request);
                return Results.Json(createdFaculty.MapToResponse(), statusCode: 201);
            })
            .Produces<ApiResponse<FacultyDto>>();

        app.MapPut("/api/v1/faculties/{facultyId:int}",
            async (int facultyId, CreateFacultyRequest request, UpdateFacultyOrchestrator orc) =>
            {
                var updatedFaculty = await orc.UpdateFacultyAsync(facultyId, request);
                return Results.Ok(updatedFaculty.MapToResponse());
            })
            .Produces<ApiResponse<FacultyDto>>();

        app.MapDelete("/api/v1/faculties/{facultyId:int}",
            async (int facultyId, DeleteFacultyOrchestrator orc) =>
            {
                await orc.DeleteFacultyAsync(facultyId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/faculties/{facultyId:int}/specialties",
            async (int facultyId, GetSpecialtiesOrchestrator orc) =>
            {
                var specialtiesResponse = await orc.GetSpecialtiesAsync(facultyId);
                return Results.Ok(specialtiesResponse.MapToResponse());
            })
            .Produces<ApiResponse<SpecialtiesResponse>>();

        app.MapPost("/api/v1/specialties",
            async (CreateSpecialtyRequest request, CreateSpecialtyOrchestrator orc) =>
            {
                var createdSpecialty = await orc.CreateSpecialtyAsync(request);
                return Results.Json(createdSpecialty.MapToResponse(), statusCode: 201);
            })
            .Produces<ApiResponse<SpecialtyDto>>();

        app.MapPut("/api/v1/specialties/{specId:int}",
            async (int specId, CreateSpecialtyRequest request, UpdateSpecialtyOrchestrator orc) =>
            {
                var updatedSpecialty = await orc.UpdatedSpecialtyAsync(specId, request);
                return Results.Ok(updatedSpecialty.MapToResponse());
            })
            .Produces<ApiResponse<SpecialtyDto>>();

        app.MapPost("/api/v1/specialties/{specId:int}/change-secret",
            async (int specId, ChangeSpecialtySecretOrchestrator orc) =>
            {
                var changedSpecialtySecret = await orc.ChangeSpecialtySecretAsync(specId);
                return Results.Ok(changedSpecialtySecret.MapToResponse());
            })
            .Produces<ApiResponse<SpecialtySecretDto>>();

        app.MapDelete("/api/v1/specialties/{specId:int}",
            async (int specId, DeleteSpecialtyOrchestrator orc) =>
            {
                await orc.DeleteSpecialtyAsync(specId);
                return Results.NoContent();
            });
    }
}
