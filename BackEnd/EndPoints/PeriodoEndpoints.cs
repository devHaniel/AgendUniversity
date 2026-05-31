using BackEnd.Dtos;
using BackEnd.Service;
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

            group.MapGet("/{id:int}", async (int id, IPeriodoService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que el periodo pertenece al usuario autenticado
                if (!await authService.IsUserPeriodoOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                var periodo = await service.GetPeriodoByIdAsync(id);
                return periodo is null ? Results.NotFound() : Results.Ok(periodo);
            });

            group.MapGet("/usuario/{usuarioId:int}", async (int usuarioId, IPeriodoService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver sus propios periodos
                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var periodos = await service.GetPeriodosByUsuarioIdAsync(usuarioId);
                return Results.Ok(periodos);
            });

            group.MapPost("/", async (PeriodoCreateDto dto, IPeriodoService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede crear periodos para sí mismo
                if (authenticatedUserId != dto.UsuarioId)
                    return Results.Forbid();

                var periodo = await service.CreateAsync(dto);
                return Results.Created($"/api/periodos/{periodo.Id}", periodo);
            });

            group.MapPut("/{id:int}", async (int id, PeriodoUpdateDto dto, IPeriodoService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que el periodo pertenece al usuario autenticado
                if (!await authService.IsUserPeriodoOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, IPeriodoService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que el periodo pertenece al usuario autenticado
                if (!await authService.IsUserPeriodoOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
