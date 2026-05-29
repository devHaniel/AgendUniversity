using BackEnd.Dtos;
using BackEnd.Models;
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

            group.MapGet("/{id}", async (int id, IUsuarioService service) =>
            {
                var usuario = await service.GetUsuarioByIdAsync(id);
                return Results.Ok(usuario);
            });

            group.MapGet("/{usuarioId}/periodos", async (int usuarioId, IUsuarioService service) =>
            {
                var usuario = await service.GetUsuarioByIdAsync(usuarioId);
                return usuario is null ? Results.NotFound() : Results.Ok(usuario.Periodos);
            });

            group.MapPost("/", async(UsuarioCreateDto dto, IUsuarioService service) =>
            {
                var usuario = await service.CreateAsync(dto);
                return Results.Created($"/api/usuarios/{usuario.Id}", usuario);

            });

            group.MapPut("/", async(UsuarioPasswordDto dto, IUsuarioService service) =>
            {
               await service.EditPasswordASync(dto);

               return Results.NoContent();
            });

            group.MapDelete("/{id}", async(int id, IUsuarioService service)=>
            {
               await service.DeleteAsync(id);
               return Results.NoContent(); 
            });

            return group;
        }
    }
}
