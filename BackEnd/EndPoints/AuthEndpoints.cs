using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using BackEnd.Service;
using BackEnd.Service.Interfaces;

namespace BackEnd.EndPoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/register", async (UsuarioCreateDto dto,
                IUsuarioService service,
                JwtService jwtService) =>
            {
                try
                {
                    var created = await service.CreateAsync(dto);

                    var usuario = await service.GetUsuarioByEmailAsync(created.Email);

                    var token = jwtService.GenerateToken(usuario);

                    var response = new RegisterResponseDto
                    {
                        Usuario = created,
                        Token = token,
                        Expiration = DateTime.UtcNow.AddMinutes(jwtService.GetExpiration())
                    };

                    return Results.Ok(response);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .AllowAnonymous();

            group.MapPost("/login", async (LoginDto dto,
                IUsuarioService service,
                JwtService jwtService,
                IPasswordService passwordService) =>
            {
                var usuario = await service.GetUsuarioByEmailAsync(dto.Email);

                if (usuario is null)
                {
                    return Results.BadRequest("Usuario no encontrado");
                }

                var isPasswordValid = passwordService.VerifyPassword(
                    usuario,
                    usuario.Password,
                    dto.Password);

                if (!isPasswordValid)
                {
                    return Results.BadRequest("Contraseña incorrecta");
                }

                var token = jwtService.GenerateToken(usuario);

                var response = new LoginResponseDto
                {
                    Usuario = new UsuarioDto
                    {
                        Id = usuario.Id,
                        Email = usuario.Email
                    },
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(jwtService.GetExpiration())
                };
                return Results.Ok(response);
            })
            .AllowAnonymous();

            return group;
        }
    }
}