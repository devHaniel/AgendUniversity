using BackEnd.Dtos;
using BackEnd.Service;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace BackEnd.EndPoints
{
    public static class TareaEndpoints
    {
        public static RouteGroupBuilder MapTareaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/tareas").RequireAuthorization();;

            group.MapGet("/", async (ITareaService service) =>
            {
                var tareas = await service.GetTareasAsync();
                return Results.Ok(tareas);
            });

            group.MapGet("/{id:guid}", async (Guid id, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la tarea pertenece al usuario autenticado
                if (!await authService.IsUserTareaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                var tarea = await service.GetTareaByIdAsync(id);
                return tarea is null ? Results.NotFound() : Results.Ok(tarea);
            });

            var uploadArchivo = group.MapPost("/{id:guid}/archivos", async (Guid id, IFormFile archivo, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserTareaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                if (archivo is null || archivo.Length == 0)
                    return Results.BadRequest("Debe seleccionar un archivo.");

                var archivoSubido = await service.SubirArchivoAsync(id, archivo);
                return Results.Ok(archivoSubido);
            });

            uploadArchivo.DisableAntiforgery();

            group.MapGet("/archivos/{archivoId:int}/download", async (int archivoId, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var archivo = await service.GetArchivoParaDescargaAsync(archivoId);
                if (archivo is null)
                    return Results.NotFound();

                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserTareaOwnerAsync(archivo.TareaId, authenticatedUserId))
                    return Results.Forbid();

                return Results.File(archivo.RutaArchivo, archivo.ContentType, archivo.NombreOriginal);
            });

            group.MapDelete("/archivos/{archivoId:int}", async (int archivoId, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var archivo = await service.GetArchivoParaDescargaAsync(archivoId);
                if (archivo is null)
                    return Results.NotFound();

                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);

                if (!await authService.IsUserTareaOwnerAsync(archivo.TareaId, authenticatedUserId))
                    return Results.Forbid();

                var resultado = await service.DeleteArchivoAsync(archivoId);
                return resultado ? Results.NoContent() : Results.BadRequest("Error al eliminar el archivo");
            });

            group.MapGet("/usuario/{usuarioId:int}/paged", async (int usuarioId, int? page, int? pageSize, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver sus propias tareas
                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var pagedTareas = await service.GetTareasByUsuarioIdPagedAsync(
                    usuarioId,
                    page.GetValueOrDefault(1),
                    pageSize.GetValueOrDefault(10));

                return Results.Ok(pagedTareas);
            });

            group.MapGet("/usuario/{usuarioId:int}", async (int usuarioId, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver sus propias tareas
                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var tareas = await service.GetTareasByUsuarioIdAsync(usuarioId);
                return Results.Ok(tareas);
            });

            group.MapGet("/asignatura/{asignaturaId:int}", async (int asignaturaId, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la asignatura pertenece al usuario autenticado
                if (!await authService.IsUserAsignaturaOwnerAsync(asignaturaId, authenticatedUserId))
                    return Results.Forbid();

                var tareas = await service.GetTareasByAsignaturaIdAsync(asignaturaId);
                return Results.Ok(tareas);
            });

            group.MapPost("/", async (TareaCreateDto dto, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la asignatura pertenece al usuario autenticado
                if (!await authService.IsUserAsignaturaOwnerAsync(dto.AsignaturaId, authenticatedUserId))
                    return Results.Forbid();

                var tarea = await service.CreateAsync(dto);
                return Results.Created($"/api/tareas/{tarea.Id}", tarea);
            });

            group.MapPut("/{id:guid}", async (Guid id, TareaUpdateDto dto, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la tarea pertenece al usuario autenticado
                if (!await authService.IsUserTareaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id:guid}", async (Guid id, ITareaService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // Verificar que la tarea pertenece al usuario autenticado
                if (!await authService.IsUserTareaOwnerAsync(id, authenticatedUserId))
                    return Results.Forbid();

                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
