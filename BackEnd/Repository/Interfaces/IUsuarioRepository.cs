using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task CreateAsync(Usuario usuario);
        void DeleteAsync(Usuario usuario);
        void EditAsync(Usuario usuario);
        Task<Usuario> GetUsuarioByEmail(string email);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<List<Usuario>> GetUsuariosAsync();
        Task SaveChangesAsync();
    }
}