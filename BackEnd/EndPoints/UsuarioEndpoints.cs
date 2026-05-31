using BackEnd.Dtos;
using BackEnd.Models;
using BackEnd.Service;
using BackEnd.Service.Interfaces;

namespace BackEnd.EndPoints
{
    public static class UsuarioEndpoints
    {
        public static RouteGroupBuilder MapUsuarioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/usuarios").RequireAuthorization();

            group.MapGet("/", async (IUsuarioService service) =>
            {
                var usuarios = await service.GetUsuariosAsync();
                return Results.Ok(usuarios);
            });

            group.MapGet("/{id}", async (int id, IUsuarioService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver su propio perfil
                if (authenticatedUserId != id)
                    return Results.Forbid();

                var usuario = await service.GetUsuarioByIdAsync(id);
                return Results.Ok(usuario);
            });

            group.MapGet("/{usuarioId}/periodos", async (int usuarioId, IUsuarioService service, AuthorizationService authService, HttpContext context) =>
            {
                var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
                
                // El usuario solo puede ver sus propios periodos
                if (authenticatedUserId != usuarioId)
                    return Results.Forbid();

                var usuario = await service.GetUsuarioByIdAsync(usuarioId);
                return usuario is null ? Results.NotFound() : Results.Ok(usuario.Periodos);
            });

            group.MapPost("/", async(UsuarioCreateDto dto, IUsuarioService service) =>
            {
                var usuario = await service.CreateAsync(dto);
                return Results.Created($"/api/usuarios/{usuario.Id}", usuario);

            });

            group.MapPut("/", async(UsuarioPasswordDto dto, IUsuarioService service, AuthorizationService authService, HttpContext context) =>
            {
               var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
               
               // Verificar que el usuario solo puede cambiar su propia contraseña
               var usuario = await service.GetUsuarioByEmailAsync(dto.Email);
               if (usuario == null || usuario.Id != authenticatedUserId)
                   return Results.Forbid();

               await service.EditPasswordASync(dto);

               return Results.NoContent();
            });

            group.MapDelete("/{id}", async(int id, IUsuarioService service, AuthorizationService authService, HttpContext context)=>
            {
               var authenticatedUserId = authService.GetAuthenticatedUserId(context.User);
               
               // El usuario solo puede eliminar su propia cuenta
               if (authenticatedUserId != id)
                   return Results.Forbid();

               await service.DeleteAsync(id);
               return Results.NoContent(); 
            });

            return group;
        }
    }
}
