using BackEnd.Dtos;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace BackEnd.EndPoints
{
    public static class AsignaturaEndpoints
    {
        public static RouteGroupBuilder MapAsignaturaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/asignaturas").RequireAuthorization();;

            group.MapGet("/", async (IAsignaturaService service) =>
            {
                var asignaturas = await service.GetAsignaturasAsync();
                return Results.Ok(asignaturas);
            });

            group.MapGet("/{id:int}", async (int id, IAsignaturaService service) =>
            {
                var asignatura = await service.GetAsignaturaByIdAsync(id);
                return asignatura is null ? Results.NotFound() : Results.Ok(asignatura);
            });

            group.MapGet("/periodo/{periodoId:int}", async (int periodoId, IAsignaturaService service) =>
            {
                var asignaturas = await service.GetAsignaturasByPeriodoIdAsync(periodoId);
                return Results.Ok(asignaturas);
            });

            group.MapPost("/", async (AsignaturaCreateDto dto, IAsignaturaService service) =>
            {
                var asignatura = await service.CreateAsync(dto);
                return Results.Created($"/api/asignaturas/{asignatura.Id}", asignatura);
            });

            group.MapPut("/{id:int}", async (int id, AsignaturaUpdateDto dto, IAsignaturaService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, IAsignaturaService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
