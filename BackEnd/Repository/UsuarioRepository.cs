using BackEnd.Models;
using BackEnd.Persistencia;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext context;

        public UsuarioRepository(AppDbContext context) 
        {
            this.context = context;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await context.Usuarios
                .Include(x => x.Periodos)
                .ThenInclude(p => p.Asignaturas)
                .ThenInclude(a => a.Tareas)
                .ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await context.Usuarios
                .Include(x => x.Periodos)
                .ThenInclude(p => p.Asignaturas)
                .ThenInclude(a => a.Tareas)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task CreateAsync(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
        }
        public void EditAsync(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
        }

        public void DeleteAsync(Usuario usuario)
        {

            context.Usuarios.Remove(usuario);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }



    }
}
