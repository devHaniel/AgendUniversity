using BackEnd.Dtos;
using BackEnd.Service;
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

            group.MapGet("/{id:int}", async (int id, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la asignatura pertenece al usuario autenticado
                if (!await authService.IsUserAsignaturaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                var asignatura = await service.GetAsignaturaByIdAsync(id);
                return asignatura is null ? Results.NotFound() : Results.Ok(asignatura);
            });

            group.MapGet("/usuario/{usuarioId:int}", async (int usuarioId, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver sus propias asignaturas
                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var asignaturas = await service.GetAsignaturasByUsuarioIdAsync(usuarioId);
                return Results.Ok(asignaturas);
            });

            group.MapGet("/periodo/{periodoId:int}", async (int periodoId, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que el periodo pertenece al usuario autenticado
                if (!await authService.IsUserPeriodoOwnerAsync(periodoId, authenticatedUserId))
                    return Results.Forbid();

                var asignaturas = await service.GetAsignaturasByPeriodoIdAsync(periodoId);
                return Results.Ok(asignaturas);
            });

            group.MapPost("/", async (AsignaturaCreateDto dto, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que el periodo pertenece al usuario autenticado
                if (!await authService.IsUserPeriodoOwnerAsync(dto.PeriodoId, authenticatedUserId))
                    return Results.Forbid();

                var asignatura = await service.CreateAsync(dto);
                return Results.Created($"/api/asignaturas/{asignatura.Id}", asignatura);
            });

            group.MapPut("/{id:int}", async (int id, AsignaturaUpdateDto dto, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la asignatura pertenece al usuario autenticado
                if (!await authService.IsUserAsignaturaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, IAsignaturaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la asignatura pertenece al usuario autenticado
                if (!await authService.IsUserAsignaturaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
