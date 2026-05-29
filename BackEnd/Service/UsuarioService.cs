using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;
using BackEnd.Repository.Interfaces;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BackEnd.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper mapper;

        public UsuarioService(IUsuarioRepository repository,
            IPasswordService passwordService,
            IMapper mapper)
        {
            this.repository = repository;
            _passwordService = passwordService;
            this.mapper = mapper;
        }

        public async Task<List<UsuarioDto>> GetUsuariosAsync()
        {
            var usuarios = await repository.GetUsuariosAsync();
            return mapper.Map<List<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var usuario = await repository.GetUsuarioByIdAsync(id);

            return mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            email = email.Trim().ToLower();

            var emailValido = new EmailAddressAttribute()
                .IsValid(email);

            if (!emailValido)
                return null;

            return await repository.GetUsuarioByEmail(email);
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioCreateDto dto)
        {
            var usuario = mapper.Map<Usuario>(dto);

            if (usuario is null)
                throw new ArgumentException(nameof(usuario));

            var emailResult = await GetUsuarioByEmailAsync(usuario.Email);

            // Email ya existente
            if (emailResult is not null)
                throw new ArgumentException("Email ya registrado.");

            usuario.Password = _passwordService.HashPassword(usuario, usuario.Password);
            await repository.CreateAsync(usuario);
            await repository.SaveChangesAsync();

            return mapper.Map<UsuarioDto>(usuario);
        }

        public async Task EditPasswordASync(UsuarioPasswordDto dto)
        {
            var usuario = await GetUsuarioByEmailAsync(dto.Email);

            if (usuario is null)
                throw new ArgumentException("Usuario no encontrado.");

            var passwordCorrecta =
            _passwordService.VerifyPassword(
            usuario,
            usuario.Password,
            dto.OldPassword);

            if (!passwordCorrecta)
                throw new ArgumentException(
                    "Contraseña incorrecta.");

            usuario.Password =
            _passwordService.HashPassword(
            usuario,
            dto.NewPassword);

            repository.EditAsync(usuario);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await repository.GetUsuarioByIdAsync(id);

            if(usuario is null)
                throw new ArgumentNullException("Usuario no encontrado.");

            repository.DeleteAsync(usuario);
            await repository.SaveChangesAsync();
        }
    }
}
