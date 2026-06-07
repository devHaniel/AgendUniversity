using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using BackEnd.Service;
using BackEnd.Service.Interfaces;

namespace BackEnd.EndPoints
{
    public static class RecordatorioEndpoints
    {
        public static RouteGroupBuilder MapRecordatorioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/recordatorios")
                .RequireAuthorization();

            group.MapGet("/", async (IRecordatorioService service) =>
            {
                var recordatorios = await service.GetRecordatoriosAsync();
                return Results.Ok(recordatorios);
            });

            group.MapGet("/{id:int}", async (
                int id,
                IRecordatorioService service,
                AuthorizationService authService,
                HttpContext context) =>
            {
                var authenticatedUserId =
                    authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserRecordatorioOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                var recordatorio = await service.GetRecordatorioByIdAsync(id);

                return recordatorio is null
                    ? Results.NotFound()
                    : Results.Ok(recordatorio);
            });

            group.MapGet("/usuario/{usuarioId:int}", async (
                int usuarioId,
                IRecordatorioService service,
                AuthorizationService authService,
                HttpContext context) =>
            {
                var authenticatedUserId =
                    authService.GetAuthenticatedUserId(context.User);

                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var recordatorios =
                    await service.GetRecordatoriosByUsuarioIdAsync(usuarioId);

                return Results.Ok(recordatorios);
            });

            group.MapPost("/", async (
                RecordatorioCreateDto dto,
                IRecordatorioService service,
                AuthorizationService authService,
                HttpContext context) =>
            {
                var authenticatedUserId =
                    authService.GetAuthenticatedUserId(context.User);

                if (authenticatedUserId != dto.UsuarioId)
                    return Results.Forbid();

                var recordatorio = await service.CreateAsync(dto);

                return Results.Created(
                    $"/api/recordatorios/{recordatorio.Id}",
                    recordatorio);
            });

            group.MapPut("/{id:int}", async (
                int id,
                RecordatorioUpdateDto dto,
                IRecordatorioService service,
                AuthorizationService authService,
                HttpContext context) =>
            {
                var authenticatedUserId =
                    authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserRecordatorioOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.UpdateAsync(id, dto);

                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (
                int id,
                IRecordatorioService service,
                AuthorizationService authService,
                HttpContext context) =>
            {
                var authenticatedUserId =
                    authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserRecordatorioOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.DeleteAsync(id);

                return Results.NoContent();
            });

            return group;
        }
    }
}