using BackEnd.Dtos;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace BackEnd.EndPoints
{
    public static class PeriodoEndpoints
    {
        public static RouteGroupBuilder MapPeriodoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/periodos").RequireAuthorization();

            group.MapGet("/", async (IPeriodoService service) =>
            {
                var periodos = await service.GetPeriodosAsync();
                return Results.Ok(periodos);
            });

            group.MapGet("/{id:int}", async (int id, IPeriodoService service) =>
            {
                var periodo = await service.GetPeriodoByIdAsync(id);
                return periodo is null ? Results.NotFound() : Results.Ok(periodo);
            });

            group.MapGet("/usuario/{usuarioId:int}", async (int usuarioId, IPeriodoService service) =>
            {
                var periodos = await service.GetPeriodosByUsuarioIdAsync(usuarioId);
                return Results.Ok(periodos);
            });

            group.MapPost("/", async (PeriodoCreateDto dto, IPeriodoService service) =>
            {
                var periodo = await service.CreateAsync(dto);
                return Results.Created($"/api/periodos/{periodo.Id}", periodo);
            });

            group.MapPut("/{id:int}", async (int id, PeriodoUpdateDto dto, IPeriodoService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, IPeriodoService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
