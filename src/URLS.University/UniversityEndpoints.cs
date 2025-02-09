using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using URLS.Shared.Api;
using URLS.University.Features.Universities.Dtos;
using URLS.University.Features.Universities.Services;

namespace URLS.University;

public static class UniversityEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/v1/university",
            async (GetUniversityOrchestrator orc) =>
            {
                var university = await orc.GetUniversityAsync();
                return Results.Ok(university.MapToResponse());
            })
            .Produces<ApiResponse<UniversityDto>>();

        app.MapPost("/api/v1/universities",
            async(CreateUniversityRequest request, CreateUniversityOrchestrator orc) =>
            {
                var createdUniversity = await orc.CreateUniversityAsync(request);
                return Results.Ok(createdUniversity.MapToResponse());
            })
            .Produces<ApiResponse<UniversityDto>>();

        app.MapPut("/api/v1/universities/{id:int}",
            async (int id, CreateUniversityRequest request, UpdateUniversityOrchestrator orc) =>
            {
                var updatedUniversity = await orc.UpdateUniversityAsync(id, request);
                return Results.Ok(updatedUniversity.MapToResponse());
            })
            .Produces<ApiResponse<UniversityDto>>();
    }
}
