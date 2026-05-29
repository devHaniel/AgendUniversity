using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using BackEnd.Models;

namespace BackEnd.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> CreateAsync(UsuarioCreateDto dto);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task<UsuarioDto> GetUsuarioByIdAsync(int id);
        Task<List<UsuarioDto>> GetUsuariosAsync();
        Task EditPasswordASync(UsuarioPasswordDto dto);
        Task DeleteAsync(int id);
    }
}